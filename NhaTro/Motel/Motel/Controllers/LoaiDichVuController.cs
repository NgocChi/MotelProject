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
    public class LoaiDichVuController : Controller
    {
        private readonly ILoaiDichVuRepository Repository = null;
        private readonly IDonViTinhRepository DonViRepository = null;
        private string _taikhoan = string.Empty;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoaiDichVuController(IHttpContextAccessor httpContextAccessor, IPhanQuyenRepository phanQuyenRepository, ILoaiDichVuRepository repository, IDonViTinhRepository donViRepository)
        {
            this.Repository = repository;
            this.DonViRepository = donViRepository;
            _httpContextAccessor = httpContextAccessor;
            this.PhanQuyenRepository = phanQuyenRepository;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");

        }
        public IActionResult Index()
        {
            CommonViewModel common = new CommonViewModel();
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            common.loaiDVViewModel.listLoaiDichVu = Repository.Gets().ToList();

            return View(common);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, LoaiDichVuViewModel loai)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    kq = await Repository.Create(loai.loaiDichVu);
                }
                else
                {
                    try
                    {
                        loai.loaiDichVu.MaLoaiDV = id;
                        kq = await Repository.Update(loai.loaiDichVu);
                    }
                    catch
                    {
                        throw;
                    }
                }
                CommonViewModel common = new CommonViewModel();
                common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                common.loaiDVViewModel.listLoaiDichVu = Repository.Gets();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", loai.loaiDichVu) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            LoaiDichVuViewModel model = new LoaiDichVuViewModel();
            model.listDonViTinh = DonViRepository.Gets();
            if (id == 0)
            {
                model.loaiDichVu = new LoaiDichVu();
                result = View(model);
            }
            else
            {
                model.loaiDichVu = await Repository.GetsById(id);
                if (model.loaiDichVu == null)
                    result = NotFound();
                result = View(model);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CommonViewModel common = new CommonViewModel();
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);

            if (id == 0)
            {
                common.loaiDVViewModel.listLoaiDichVu = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
            }
            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                common.loaiDVViewModel.listLoaiDichVu = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", common) });

            }
        }
    }
}