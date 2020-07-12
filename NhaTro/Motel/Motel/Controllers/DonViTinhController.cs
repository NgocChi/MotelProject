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
    public class DonViTinhController : Controller
    {
        private readonly IDonViTinhRepository Repository = null;

        public DonViTinhController(IDonViTinhRepository repository)
        {
            this.Repository = repository;

        }
        public IActionResult Index()
        {
            DonViTinhViewModel model = new DonViTinhViewModel();
            model.listDonViTinh = Repository.Gets().ToList();
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
                DonViTinhViewModel model = new DonViTinhViewModel();
                model.listDonViTinh = Repository.Gets();
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
            DonViTinhViewModel model = new DonViTinhViewModel();
            if (id == 0)
            {
                model.listDonViTinh = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                model.listDonViTinh = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });

            }
        }
    }
}