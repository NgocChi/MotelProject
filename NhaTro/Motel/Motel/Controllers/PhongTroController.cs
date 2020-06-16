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
    public class PhongTroController : Controller
    {
        private readonly IPhongRepository Repository = null;
        private readonly INhaTroRepository NhaTroRepository = null;
        public PhongTroController(IPhongRepository repository, INhaTroRepository nhaTroRepository)
        {
            this.Repository = repository;
            this.NhaTroRepository = nhaTroRepository;
        }
        public ViewResult Index()
        {
            QuanLyPhongViewModel model = new QuanLyPhongViewModel();
            model.listPhong = Repository.Gets();
            model.listLoaiPhong = Repository.GetsLoaiPhong();
            model.listNhaTro = NhaTroRepository.Gets();
            model.listTrangThaiPhong = Repository.GetsTrangThaiPhong();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Phong phong)
        {
            int kq = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["info"] = "Dữ liệu bị trống";
                    return RedirectToAction(nameof(PhongTroController.Create), "PhongTro");
                }
                kq = Repository.Create(phong);
                if (kq == 1)
                {
                   // NhaTroRepository.UpdateSoLuongPhong();
                    ViewData["info"] = "Thêm nhà trọ thành công";
                    return RedirectToAction(nameof(PhongTroController.Index), "PhongTro");
                }
                else
                {
                    ViewData["info"] = "Thêm thất bại";
                    return RedirectToAction(nameof(PhongTroController.Create), "PhongTro");
                }

            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return RedirectToAction(nameof(PhongTroController.Create), "PhongTro");
            }

        }
        [HttpPost]
        public IActionResult Update(Phong phong)
        {
            int kq = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["info"] = "Dữ liệu bị trống";
                    return RedirectToAction(nameof(PhongTroController.Update), "PhongTro");
                }
                kq = Repository.Update(phong);
                if (kq == 1)
                {
                    ViewData["info"] = "Cập nhật nhà trọ thành công";
                    return RedirectToAction(nameof(PhongTroController.Index), "PhongTro");
                }
                else
                {
                    ViewData["info"] = "Cập nhật thất bại";
                    return RedirectToAction(nameof(PhongTroController.Update), "PhongTro");
                }

            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return RedirectToAction(nameof(PhongTroController.Update), "PhongTro");
            }

        }

        [HttpPost]
        public IActionResult CreateLoaiPhong(LoaiPhong loaiPhong)
        {
            int kq = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["info"] = "Dữ liệu bị trống";
                    return RedirectToAction(nameof(PhongTroController.CreateLoaiPhong), "PhongTro");
                }
                kq = Repository.CreateLoaiPhong(loaiPhong);
                if (kq == 1)
                {
                    ViewData["info"] = "Thêm nhà trọ thành công";
                    return RedirectToAction(nameof(PhongTroController.Index), "PhongTro");
                }
                else
                {
                    ViewData["info"] = "Thêm thất bại";
                    return RedirectToAction(nameof(PhongTroController.CreateLoaiPhong), "PhongTro");
                }

            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return RedirectToAction(nameof(PhongTroController.CreateLoaiPhong), "PhongTro");
            }

        }
        [HttpPost]
        public IActionResult UpdateLoaiPhong(LoaiPhong PhongTro)
        {
            int kq = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["info"] = "Dữ liệu bị trống";
                    return RedirectToAction(nameof(PhongTroController.UpdateLoaiPhong), "PhongTro");
                }
                kq = Repository.UpdateLoaiPhong(PhongTro);
                if (kq == 1)
                {
                    ViewData["info"] = "Cập nhật nhà trọ thành công";
                    return RedirectToAction(nameof(PhongTroController.Index), "PhongTro");
                }
                else
                {
                    ViewData["info"] = "Cập nhật thất bại";
                    return RedirectToAction(nameof(PhongTroController.UpdateLoaiPhong), "PhongTro");
                }

            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return RedirectToAction(nameof(PhongTroController.UpdateLoaiPhong), "PhongTro");
            }

        }

    }
}