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
    public class DatPhongController : Controller
    {
        private readonly IDatPhongRepository Repository = null;
        private readonly IPhongRepository PhongRepository = null;
        private readonly IKhachHangRepository KhachHangRepository = null;
        public DatPhongController(IDatPhongRepository repository, IPhongRepository phongRepository, IKhachHangRepository khachHangRepository)
        {
            this.Repository = repository;
            this.PhongRepository = phongRepository;
            this.KhachHangRepository = khachHangRepository;
        }

        public IActionResult Index()
        {
            QuanLyDatPhongViewModel dp = new QuanLyDatPhongViewModel();
            dp.listDatPhong = Repository.Gets();
            return View(dp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, QuanLyDatPhongViewModel datPhong)
        {
            int kq = -1;
            QuanLyDatPhongViewModel dp = new QuanLyDatPhongViewModel();
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    if (datPhong.khachHangDatPhong.datPhong._MaKH == 0)
                    {
                        int makh = await KhachHangRepository.Create(datPhong.khachHangDatPhong.khachHang);
                        datPhong.khachHangDatPhong.datPhong._MaKH = makh;
                    }
                    await Repository.Create(datPhong.khachHangDatPhong.datPhong);
                    await PhongRepository.UpdateTTP(datPhong.khachHangDatPhong.datPhong._MaPH, 2);
                }
                else
                {
                    try
                    {
                        datPhong.khachHangDatPhong.datPhong.MaDP = id;
                        kq = await Repository.Update(datPhong.khachHangDatPhong.datPhong);
                    }
                    catch
                    {
                        throw;
                    }
                }
                dp.listDatPhong = Repository.Gets();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", dp) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", datPhong) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            QuanLyDatPhongViewModel model = new QuanLyDatPhongViewModel();
            model.listPhong = PhongRepository.GetsPhongTrong();
            model.listKhachHang = KhachHangRepository.Gets();
            model.listDatPhong = Repository.Gets();
            if (id == 0)
            {
                model.khachHangDatPhong = new KhachHangDatPhongViewModel();
                model.khachHangDatPhong.datPhong = new DatPhong();
                model.khachHangDatPhong.khachHang = new KhachHang();
                result = View(model);
            }
            else
            {
                model.khachHangDatPhong = new KhachHangDatPhongViewModel();

                model.khachHangDatPhong.datPhong = await Repository.GetsById(id);
                model.khachHangDatPhong.khachHang = await KhachHangRepository.GetsById(model.khachHangDatPhong.datPhong._MaKH);
                if (model.khachHangDatPhong.datPhong == null)
                    result = NotFound();
                result = View(model);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            QuanLyDatPhongViewModel dp = new QuanLyDatPhongViewModel();
            if (id == 0)
            {
                dp.listDatPhong = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", dp) });
            }

            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                dp.listDatPhong = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", dp) });

            }
        }
    }
}