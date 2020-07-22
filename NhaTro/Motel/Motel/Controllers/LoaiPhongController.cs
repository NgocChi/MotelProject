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
    public class LoaiPhongController : Controller
    {
        private readonly ILoaiPhongRepository Repository = null;
        private readonly INhaTroRepository NhaTroRepository = null;
        private string _taikhoan = string.Empty;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoaiPhongController(IHttpContextAccessor httpContextAccessor, IPhanQuyenRepository phanQuyenRepository, ILoaiPhongRepository repository, INhaTroRepository nhaTroRepository)
        {
            this.Repository = repository;
            this.NhaTroRepository = nhaTroRepository;
            _httpContextAccessor = httpContextAccessor;
            this.PhanQuyenRepository = phanQuyenRepository;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");
        }
        public ViewResult Index()
        {
            CommonViewModel common = new CommonViewModel();
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            common.qlLoaiPhongViewModel.listLoaiPhong = Repository.GetsLoaiPhong();

            return View(common);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditLoaiPh(int id, QuanLyLoaiPhongViewModel loaiph)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    kq = await Repository.CreateLoaiPhong(loaiph.loaiPhong);
                }
                else
                {
                    try
                    {
                        loaiph.loaiPhong.MaLP = id;
                        kq = await Repository.UpdateLoaiPhong(loaiph.loaiPhong);
                    }
                    catch
                    {
                        throw;
                    }
                }
                CommonViewModel common = new CommonViewModel();
                common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                common.qlLoaiPhongViewModel.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", loaiph.loaiPhong) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEditLoaiPh(int id)
        {
            IActionResult result;
            QuanLyLoaiPhongViewModel model = new QuanLyLoaiPhongViewModel();
            if (id == 0)
            {
                model.loaiPhong = new LoaiPhong();
                result = View(model);
            }
            else
            {
                model.loaiPhong = await Repository.GetLoaiPhById(id);
                if (model.loaiPhong == null)
                    result = NotFound();
                result = View(model);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLoaiPh(int id)
        {
            CommonViewModel common = new CommonViewModel();
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);

            if (id == 0)
            {
                common.qlLoaiPhongViewModel.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
            }
            else
            {
                int checkForeign = Repository.CheckForeignKey(id);
                if (checkForeign == 1)
                {
                    int kq = await Repository.DeleteLoaiPh(id);
                    if (kq == 0)
                        return NotFound();
                    common.qlLoaiPhongViewModel.listLoaiPhong = Repository.GetsLoaiPhong();
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
                }
                else
                {
                    common.qlLoaiPhongViewModel.listLoaiPhong = Repository.GetsLoaiPhong();
                    return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
                }
            }
        }
    }
}