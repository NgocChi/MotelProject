using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.ViewModels;

namespace Motel.Controllers
{
    public class HopDongController : Controller
    {
        private readonly IHopDongRepository Repository = null;
        private readonly IPhongRepository PhongRepository = null;
        private readonly IKhachHangRepository KhachHangRepository = null;
        public HopDongController(IHopDongRepository repository, IPhongRepository phongRepository, IKhachHangRepository khachHangRepository)
        {
            this.Repository = repository;
            this.PhongRepository = phongRepository;
            this.KhachHangRepository = khachHangRepository;
        }
        public IActionResult Index()
        {
            QuanLyHopDongViewModel hd = new QuanLyHopDongViewModel();
            hd.listHopDong = Repository.Gets();
            hd.listPhong = PhongRepository.GetsPhongTrong();
            hd.listKhachHang = KhachHangRepository.Gets();
            return View(hd);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, QuanLyHopDongViewModel hd)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    //kq = await Repository.Create(hd.phong);
                    //await NhaTroRepository.UpdateSoLuongPhong(hd.phong._MaNT, 1);
                }
                else
                {
                    try
                    {
                        //ph.phong.MaPH = id;
                        //kq = await Repository.Update(ph.phong);
                    }
                    catch
                    {
                        throw;
                    }
                }
                QuanLyHopDongViewModel model = new QuanLyHopDongViewModel();
                //model.listPhong = Repository.Gets();
                //model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", hd) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            QuanLyHopDongViewModel model = new QuanLyHopDongViewModel();
            //model.listLoaiPhong = Repository.GetsLoaiPhong();
            //model.listNhaTro = NhaTroRepository.Gets();
            //model.listTrangThaiPhong = Repository.GetsTrangThaiPhong();
            if (id == 0)
            {
                //model.phong = new Phong();
                result = View(model);
            }
            else
            {
                //model.phong = await Repository.GetById(id);
                //if (model.phong == null)
                //    result = NotFound();
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
                //model.listPhong = Repository.Gets();
                //model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                //int kq = await Repository.Delete(id);
                //if (kq == 0)
                //    return NotFound();
                //model.listPhong = Repository.Gets();
                //model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });

            }
        }

    }
}