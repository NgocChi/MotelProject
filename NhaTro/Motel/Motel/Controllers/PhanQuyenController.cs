using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;

namespace Motel.Controllers
{
    public class PhanQuyenController : Controller
    {
        private readonly INhomNguoiDungRepository Repository = null;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;

        public PhanQuyenController(INhomNguoiDungRepository repository, IPhanQuyenRepository phanQuyenRepository)
        {
            this.Repository = repository;
            this.PhanQuyenRepository = phanQuyenRepository;

        }
        public IActionResult Index()
        {
            QuanLyNhomNguoiDungViewModel model = new QuanLyNhomNguoiDungViewModel();
            model.listNhomNguoiDung = Repository.Gets();
            model.listPhanQuyen = PhanQuyenRepository.GetsManHinh(0);
            return View(model);
        }

        public IActionResult Table(int idNhomNguoiDung = 0)
        {
            QuanLyNhomNguoiDungViewModel model = new QuanLyNhomNguoiDungViewModel();
            model.listPhanQuyen = PhanQuyenRepository.GetsManHinh(idNhomNguoiDung);
            model.MaNhomNguoiDung = idNhomNguoiDung;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, QuanLyNhomNguoiDungViewModel model)
        {

            try
            {
                if (id != 0)
                {
                    foreach (var item in model.listPhanQuyen)
                    {
                        int check = PhanQuyenRepository.CheckForeignKey(id, item.MaManHinh);

                        if (check == 0)
                        {
                            PhanQuyen pq = new PhanQuyen();
                            pq.MaNhomNguoiDung = id;
                            pq.MaManHinh = item.MaManHinh;
                            pq.CoQuyen = item.CoQuyen;
                            await PhanQuyenRepository.Update(pq);
                        }
                        else
                        {
                            if (item.CoQuyen == true)
                            {
                                PhanQuyen pq = new PhanQuyen();
                                pq.MaNhomNguoiDung = id;
                                pq.MaManHinh = item.MaManHinh;
                                pq.CoQuyen = item.CoQuyen;
                                await PhanQuyenRepository.Create(pq);
                            }
                        }
                    }
                }
                QuanLyNhomNguoiDungViewModel phanQuyenModel = new QuanLyNhomNguoiDungViewModel();
                phanQuyenModel.listNhomNguoiDung = Repository.Gets();
                phanQuyenModel.listPhanQuyen = PhanQuyenRepository.GetsManHinh(id);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "Table", phanQuyenModel) });
            }
            catch
            {
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Table", model) });
            }

        }
    }
}