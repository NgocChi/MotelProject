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
        private string _taikhoan = string.Empty;

        public DangNhapController(ITaiKhoanRepository repository, INhaTroRepository nhaTroRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.Repository = repository;
            this.NhaTroRepository = nhaTroRepository;
            _httpContextAccessor = httpContextAccessor;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");
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

            if (taikhoan.TenTaiKhoan == null)
                return View(taikhoan);

            var user = Repository.DangNhap(taikhoan.TenTaiKhoan, taikhoan.MatKhau);

            if (user == null)
            {
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Login", model) });
            }
            _httpContextAccessor.HttpContext.Session.SetComplexData("UserData", taikhoan.TenTaiKhoan);
            model.listNhaTro = NhaTroRepository.GetsList(taikhoan.TenTaiKhoan);
            return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ChooseMotel", model) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Choose(QuanLyTaiKhoan tk)
        {

            _httpContextAccessor.HttpContext.Session.SetComplexData("MotelData", tk._chooseMotel);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            QuanLyTaiKhoan model = new QuanLyTaiKhoan();
            model.taikhoan = new TaiKhoanViewModel();
            model.chuTro = new ChuTro();
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
                taikh.LoaiTaiKhoan = true;
                taikh._MaNND = 1;
                kq = Repository.Create(taikh);
                if (kq == 1)
                {
                    tk.chuTro._TenTaiKhoan = tk.taikhoan.TenTaiKhoan;
                    Repository.CreateChuTro(tk.chuTro);
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", tk) });
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Ten", "DiaChi", "TongPhong", "PhongTrong", "Mota")] NhaTro nhaTroViewModel)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    int _maChuTro = Repository.GetByTaiKhoan(_taikhoan).MaChuTro;
                    nhaTroViewModel._MaChuTro = _maChuTro;
                    kq = await NhaTroRepository.Create(nhaTroViewModel);
                }
                else
                {
                    try
                    {
                        nhaTroViewModel.MaNT = id;
                        kq = await NhaTroRepository.Update(nhaTroViewModel);
                    }
                    catch
                    {
                        throw;
                    }
                }
                QuanLyTaiKhoan nt = new QuanLyTaiKhoan();
                nt.listNhaTro = NhaTroRepository.GetsList(_taikhoan);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ChooseMotel", nt) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", nhaTroViewModel) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            if (id == 0)
            {
                return View(new NhaTro());
            }
            else
            {
                var kq = await NhaTroRepository.GetsById(id);
                if (kq == null)
                    result = NotFound();
                result = View(kq);
            }
            return result;
        }



    }
}
