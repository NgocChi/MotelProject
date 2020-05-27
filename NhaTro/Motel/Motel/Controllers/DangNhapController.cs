using Microsoft.AspNetCore.Mvc;
using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Controllers
{
    public class DangNhapController : Controller
    {
        public IActionResult DangNhap()
        {
            return View();

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Login(TaiKhoan tk)
        {
            return View();

        }
    }
}
