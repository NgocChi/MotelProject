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
    public class HopDongController : Controller
    {
        private readonly IHopDongRepository Repository = null;
        private readonly IPhongRepository PhongRepository = null;
        private readonly IKhachHangRepository KhachHangRepository = null;
        private readonly IDichVuRepository DichVuRepository = null;
        public HopDongController(IHopDongRepository repository, IPhongRepository phongRepository, IKhachHangRepository khachHangRepository, IDichVuRepository dichVuRepository)
        {
            this.Repository = repository;
            this.PhongRepository = phongRepository;
            this.KhachHangRepository = khachHangRepository;
            this.DichVuRepository = dichVuRepository;
        }
        public IActionResult Index()
        {
            QuanLyHopDongViewModel hd = new QuanLyHopDongViewModel();
            hd.listHopDong = Repository.Gets();

            return View(hd);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, QuanLyHopDongViewModel hopdong)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    kq = await Repository.Create(hopdong.hopDongKhachHangPhong.hopDong);

                }
                else
                {
                    try
                    {
                        hopdong.hopDongKhachHangPhong.hopDong.MaHopDong = id;
                        kq = await Repository.Update(hopdong.hopDongKhachHangPhong.hopDong);
                    }
                    catch
                    {
                        throw;
                    }
                }
                QuanLyHopDongViewModel model = new QuanLyHopDongViewModel();
                model.listPhong = PhongRepository.GetsPhongTrong();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", hopdong) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            QuanLyHopDongViewModel model = new QuanLyHopDongViewModel();
            model.listPhong = PhongRepository.GetsPhongTrong();
            model.listKhachHang = KhachHangRepository.Gets();
            model.listDichVu = DichVuRepository.Gets();
            if (id == 0)
            {
                model.hopDongKhachHangPhong = new HopDongKhachHang();
                model.hopDongKhachHangPhong.datPhong = new DatPhong();
                model.hopDongKhachHangPhong.khachHang = new KhachHang();
                model.hopDongKhachHangPhong.phong = new Phong();
                model.hopDongKhachHangPhong.hopDong = new HopDong();
                result = View(model);
            }
            else
            {
                model.hopDongKhachHangPhong.hopDong = await Repository.GetById(id);
                if (model.hopDongKhachHangPhong.hopDong == null)
                    result = NotFound();
                result = View(model);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            QuanLyHopDongViewModel model = new QuanLyHopDongViewModel();
            if (id == 0)
            {
                model.listHopDong = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                model.listHopDong = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
        }

    }
}