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
    public class TaoTaiKhoanController : Controller
    {
        private readonly ITaiKhoanRepository Repository = null;

        public TaoTaiKhoanController(ITaiKhoanRepository repository)
        {
            Repository = repository;

        }
        public IActionResult CreateAccount()
        {

            return View();
        }

        public IActionResult Create_Account(TaiKhoanViewModel taikhoan)
        {
            int kq = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["info"] = "Dữ liệu bị trống";
                    return RedirectToAction(nameof(TaoTaiKhoanController.CreateAccount), "TaoTaiKhoan");
                }
                if (taikhoan.MatKhau != taikhoan.PrMatKhau)
                {
                    ViewData["info"] = "Mật khẩu không khớp";
                    return RedirectToAction(nameof(TaoTaiKhoanController.CreateAccount), "TaoTaiKhoan");
                }
                TaiKhoan tk = new TaiKhoan();
                tk.TenTaiKhoan = taikhoan.TenTaiKhoan;
                tk.MatKhau = taikhoan.MatKhau;
                kq = Repository.Create(tk);
                if(kq == 1)
                {
                    ViewData["info"] = "Suscess";
                    return RedirectToAction(nameof(DangNhapController.Login), "DangNhap");
                }
                else
                {
                    ViewData["info"] = "Tài khoản đã tồn tại";
                    return RedirectToAction(nameof(TaoTaiKhoanController.CreateAccount), "TaoTaiKhoan");
                }
                   
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return RedirectToAction(nameof(TaoTaiKhoanController.CreateAccount), "TaoTaiKhoan");
            }
       
        }
    }
}