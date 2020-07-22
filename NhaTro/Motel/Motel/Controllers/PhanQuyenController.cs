using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;
using Web;

namespace Motel.Controllers
{
    public class PhanQuyenController : Controller
    {
        private readonly INhomNguoiDungRepository Repository = null;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;
        private string _taikhoan = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhanQuyenController(IHttpContextAccessor httpContextAccessor, INhomNguoiDungRepository repository, IPhanQuyenRepository phanQuyenRepository)
        {
            this.Repository = repository;
            this.PhanQuyenRepository = phanQuyenRepository;
            _httpContextAccessor = httpContextAccessor;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");

        }
        public IActionResult Index()
        {
            CommonViewModel common = new CommonViewModel();
            common.qlPhanQuyenViewModel.listNhomNguoiDung = Repository.Gets();
            common.qlPhanQuyenViewModel.listPhanQuyen = PhanQuyenRepository.GetsManHinh(0);
            return View(common);
        }

        public IActionResult Table(int idNhomNguoiDung = 0)
        {
            CommonViewModel common = new CommonViewModel();
            common.qlPhanQuyenViewModel.listNhomNguoiDung = Repository.Gets();
            common.qlPhanQuyenViewModel.listPhanQuyen = PhanQuyenRepository.GetsManHinh(idNhomNguoiDung);
            common.qlPhanQuyenViewModel.MaNhomNguoiDung = idNhomNguoiDung;
            return View(common);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, CommonViewModel model)
        {

            try
            {
                if (id != 0)
                {
                    foreach (var item in model.qlPhanQuyenViewModel.listPhanQuyen)
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
                CommonViewModel common = new CommonViewModel();
                common.qlPhanQuyenViewModel.listNhomNguoiDung = Repository.Gets();
                common.qlPhanQuyenViewModel.listPhanQuyen = PhanQuyenRepository.GetsManHinh(id);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "Table", common) });
            }
            catch
            {
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Table", model) });
            }

        }
    }
}