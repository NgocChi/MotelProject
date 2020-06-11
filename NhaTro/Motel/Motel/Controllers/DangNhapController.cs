using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controls;

namespace Motel.Controllers
{
    public class DangNhapController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITaiKhoanRepository Repository = null;

        public DangNhapController(ITaiKhoanRepository repository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            Repository = repository;

        }

        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Login(TaiKhoan taikhoan)
        {
            if (!ModelState.IsValid)
                return View(taikhoan);

            var user = Repository.DangNhap(taikhoan.TenTaiKhoan,taikhoan.MatKhau);

            if (user != null)
            {
                 return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Username/password not found");
            return View(taikhoan);

        }

    }
}
