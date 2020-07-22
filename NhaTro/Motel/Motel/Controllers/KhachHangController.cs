using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.Queries;
using Motel.Repositories;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web;
using Web.Controls;

namespace Motel.Controllers
{

    public class KhachHangController : Controller
    {
        private readonly IKhachHangRepository Repository = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int _nhaTro = 0;
        private string _taikhoan = string.Empty;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;

        public KhachHangController(IKhachHangRepository queries, IPhanQuyenRepository phanQuyenRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.Repository = queries;
            _httpContextAccessor = httpContextAccessor;
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("MotelData");
            this.PhanQuyenRepository = phanQuyenRepository;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");
        }

        public ViewResult Index()
        {
            CommonViewModel model = new CommonViewModel();
            model.qlKhachHangViewModel.list = Repository.Gets();
            model.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, KhachHangViewModel ViewModel)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    kq = await Repository.Create(ViewModel.khachHang);
                }
                else
                {
                    try
                    {
                        ViewModel.khachHang.MaKh = id;
                        kq = await Repository.Update(ViewModel.khachHang);
                    }
                    catch
                    {
                        throw;
                    }
                }
                CommonViewModel model = new CommonViewModel();
                model.qlKhachHangViewModel.list = Repository.Gets();
                model.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", ViewModel) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            KhachHangViewModel kh = new KhachHangViewModel();
            if (id == 0)
            {
                kh.khachHang = new KhachHang();
                return View(kh);
            }
            else
            {
                kh.khachHang = await Repository.GetsById(id);
                if (kh.khachHang == null)
                    result = NotFound();
                result = View(kh);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CommonViewModel model = new CommonViewModel();

            model.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            if (id == 0)
            {
                model.qlKhachHangViewModel.list = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int checkForeign = Repository.CheckForeignKey(id);
                if (checkForeign == 1)
                {
                    int kq = await Repository.Delete(id);
                    if (kq == 0)
                        return NotFound();
                    model.qlKhachHangViewModel.list = Repository.Gets();
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
                }
                else
                {
                    model.qlKhachHangViewModel.list = Repository.Gets();
                    return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
                }
            }
        }






    }
}
