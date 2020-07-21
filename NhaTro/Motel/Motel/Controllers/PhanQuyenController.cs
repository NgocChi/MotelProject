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
            model.listPhanQuyen = PhanQuyenRepository.GetsManHinh();
            return View(model);
        }

        public IActionResult Index1(int idNhomNguoiDung = 0)
        {
            QuanLyNhomNguoiDungViewModel model = new QuanLyNhomNguoiDungViewModel();
            model.listPhanQuyen = PhanQuyenRepository.GetsManHinh();

            return Json(new { html = Helper.RenderRazorViewToString(this, "Table", model) });
        }
    }
}