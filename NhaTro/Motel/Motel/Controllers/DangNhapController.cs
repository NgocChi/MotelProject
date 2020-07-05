using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.Queries;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web;
using Web.Controls;

namespace Motel.Controllers
{
    public class DangNhapController : Controller
    {

        private readonly ITaiKhoanRepository Repository = null;
        private readonly INhaTroRepository NhaTroRepository = null;

        public DangNhapController(ITaiKhoanRepository repository, INhaTroRepository nhaTroRepository)
        {
            this.Repository = repository;
            this.NhaTroRepository = nhaTroRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(TaiKhoanViewModel taikhoan)
        {
            QuanLyTaiKhoan model = new QuanLyTaiKhoan();
            model.listNhaTro = NhaTroRepository.Gets();
            if (taikhoan.TenTaiKhoan == null)
                return View(taikhoan);

            var user = Repository.DangNhap(taikhoan.TenTaiKhoan, taikhoan.MatKhau);

            if (user == null)
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Login", model) });
            return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ChooseMotel", model) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Choose(QuanLyTaiKhoan tk)
        {
            HttpContext.Session.SetComplexData("UserData", tk._chooseMotel);
            if (tk._chooseMotel == 0)
                return RedirectToAction("Login", "DangNhap");
            return RedirectToAction("Index", "Home");
        }

    }
}
