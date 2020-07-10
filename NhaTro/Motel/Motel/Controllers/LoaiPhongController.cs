using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;

namespace Motel.Controllers
{
    public class LoaiPhongController : Controller
    {
        private readonly ILoaiPhongRepository Repository = null;
        private readonly INhaTroRepository NhaTroRepository = null;

        public LoaiPhongController(ILoaiPhongRepository repository, INhaTroRepository nhaTroRepository)
        {
            this.Repository = repository;
            this.NhaTroRepository = nhaTroRepository;
        }
        public ViewResult Index()
        {
            QuanLyLoaiPhongViewModel model = new QuanLyLoaiPhongViewModel();
            model.listLoaiPhong = Repository.GetsLoaiPhong();
            return View(model);
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
                QuanLyLoaiPhongViewModel model = new QuanLyLoaiPhongViewModel();
                model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
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
            QuanLyLoaiPhongViewModel model = new QuanLyLoaiPhongViewModel();
            if (id == 0)
            {
                model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int kq = await Repository.DeleteLoaiPh(id);
                if (kq == 0)
                    return NotFound();
                model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });

            }
        }
    }
}