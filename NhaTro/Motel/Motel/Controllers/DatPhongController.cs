using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            dp.listPhong = PhongRepository.GetsPhongTrong();
            dp.listKhachHang = KhachHangRepository.Gets();
            return View(dp);
        }

        [HttpPost]
        public JsonResult LoadComboKhachHang()
        {
            var result = KhachHangRepository.Gets();
            return Json(new
            {
                listKh = result
            }); ;
        }

        [HttpPost]
        public IActionResult Create(KhachHangDatPhongViewModel dp)
        {
            int kq = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["info"] = "Dữ liệu bị trống";
                    return RedirectToAction(nameof(DatPhongController.Create), "DatPhong");
                }
                if(dp.datPhong._MaKH ==0)
                {
                    var addKH = KhachHangRepository.Create(dp.khachHang).GetHashCode();
                }
                kq = Repository.Create(dp.datPhong);
                if (kq == 1)
                {
                    PhongRepository.UpdateTTP(dp.datPhong._MaPH,2);
                    ViewData["info"] = "Thêm nhà trọ thành công";
                    return RedirectToAction(nameof(DatPhongController.Index), "DatPhong");
                }
                else
                {
                    ViewData["info"] = "Thêm thất bại";
                    return RedirectToAction(nameof(DatPhongController.Create), "DatPhong");
                }
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return RedirectToAction(nameof(DatPhongController.Create), "DatPhong");
            }
        }
    }
}