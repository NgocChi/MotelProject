using DAL;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DangNhapController(ITaiKhoanRepository repository, INhaTroRepository nhaTroRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.Repository = repository;
            this.NhaTroRepository = nhaTroRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(TaiKhoanViewModel taikhoan)
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
        public IActionResult Choose(QuanLyTaiKhoan tk)
        {

            _httpContextAccessor.HttpContext.Session.SetComplexData("UserData", tk._chooseMotel);
            //if (tk._chooseMotel == 0)
            //    return RedirectToAction("Login", "DangNhap");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            QuanLyTaiKhoan model = new QuanLyTaiKhoan();
            model.taikhoan = new TaiKhoanViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAccount(QuanLyTaiKhoan tk)
        {
            int kq = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "CreateAccount", tk) });
                }
                if (tk.taikhoan.MatKhau != tk.taikhoan.PrMatKhau)
                {
                    return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "CreateAccount", tk) });
                }
                TaiKhoan taikh = new TaiKhoan();
                taikh.TenTaiKhoan = tk.taikhoan.TenTaiKhoan;
                taikh.MatKhau = tk.taikhoan.MatKhau;
                kq = Repository.Create(taikh);
                if (kq == 1)
                {
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "Login", tk) });
                }
                else
                {
                    return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "CreateAccount", tk) });

                }

            }
            catch (Exception ex)
            {
                return View();
            }
        }



    }
}
