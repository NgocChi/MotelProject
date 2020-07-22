using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;
using Web;

namespace Motel.Controllers
{
    public class DonViTinhController : Controller
    {
        private readonly IDonViTinhRepository Repository = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int _nhaTro = 0;
        private string _taikhoan = string.Empty;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;

        public DonViTinhController(IDonViTinhRepository repository, IPhanQuyenRepository phanQuyenRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.Repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("MotelData");
            this.PhanQuyenRepository = phanQuyenRepository;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");

        }
        public IActionResult Index()
        {
            CommonViewModel model = new CommonViewModel();
            model.donViTinhViewModel.listDonViTinh = Repository.Gets().ToList();
            model.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, DonViTinhViewModel dvt)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    kq = await Repository.Create(dvt.donViTinh);
                }
                else
                {
                    try
                    {
                        dvt.donViTinh.MaDonVi = id;
                        kq = await Repository.Update(dvt.donViTinh);
                    }
                    catch
                    {
                        throw;
                    }
                }
                CommonViewModel model = new CommonViewModel();
                model.donViTinhViewModel.listDonViTinh = Repository.Gets().ToList();
                model.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", dvt.donViTinh) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            DonViTinhViewModel model = new DonViTinhViewModel();
            if (id == 0)
            {
                model.donViTinh = new DonViTinh();
                result = View(model);
            }
            else
            {
                model.donViTinh = await Repository.GetsById(id);
                if (model.donViTinh == null)
                    result = NotFound();
                result = View(model);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CommonViewModel model = new CommonViewModel();
            model.donViTinhViewModel.listDonViTinh = Repository.Gets().ToList();
            model.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            if (id == 0)
            {
                model.donViTinhViewModel.listDonViTinh = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                model.donViTinhViewModel.listDonViTinh = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });

            }
        }
    }
}