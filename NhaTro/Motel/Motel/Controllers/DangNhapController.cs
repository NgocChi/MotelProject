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
using Web.Controls;

namespace Motel.Controllers
{
    public class DangNhapController : Controller
    {

        private readonly ITaiKhoanRepository Repository = null;
        public HomeController Home = new HomeController();

        public DangNhapController(ITaiKhoanRepository repository)
        {
            Repository = repository;

        }

        [HttpGet]
        public async Task<IActionResult> Login(TaiKhoanViewModel taikhoan)
        {
            if (taikhoan.TenTaiKhoan == null)
                return View(taikhoan);

            var user = Repository.DangNhap(taikhoan.TenTaiKhoan, taikhoan.MatKhau);
            if (user == null)
                return View(taikhoan);
            return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(Home, "Index", null) });
        }

    }
}
