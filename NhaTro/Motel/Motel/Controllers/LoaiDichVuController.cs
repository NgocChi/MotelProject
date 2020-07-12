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
    public class LoaiDichVuController : Controller
    {
        private readonly ILoaiDichVuRepository Repository = null;
        private readonly IDonViTinhRepository DonViRepository = null;

        public LoaiDichVuController(ILoaiDichVuRepository repository, IDonViTinhRepository donViRepository)
        {
            this.Repository = repository;
            this.DonViRepository = donViRepository;

        }
        public IActionResult Index()
        {
            LoaiDichVuViewModel model = new LoaiDichVuViewModel();
            model.listLoaiDichVu = Repository.Gets().ToList();
            return View(model);
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
                LoaiDichVuViewModel model = new LoaiDichVuViewModel();
                model.listLoaiDichVu = Repository.Gets();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
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
            LoaiDichVuViewModel model = new LoaiDichVuViewModel();
            if (id == 0)
            {
                model.listLoaiDichVu = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                model.listLoaiDichVu = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });

            }
        }
    }
}