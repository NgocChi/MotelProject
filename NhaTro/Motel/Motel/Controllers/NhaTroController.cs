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
    public class NhaTroController : Controller
    {
        private readonly INhaTroRepository Repository = null;
        NhaTroViewModel nt = null;
        public NhaTroController(INhaTroRepository repository)
        {
            this.Repository = repository;
            nt = new NhaTroViewModel();
        }

        public ViewResult Index()
        {
            nt.listNhaTro = Repository.GetsList();
            ViewResult kq = View(nt);
            return kq;
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
                    kq = await Repository.Create(nhaTroViewModel);
                }
                else
                {
                    try
                    {
                        nhaTroViewModel.MaNT = id;
                        kq = await Repository.Update(nhaTroViewModel);
                    }
                    catch
                    {
                        throw;
                    }
                }
                NhaTroViewModel nt = new NhaTroViewModel();
                nt.listNhaTro = Repository.GetsList();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", nt) });
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
                var kq = await Repository.GetsById(id);
                if (kq == null)
                    result = NotFound();
                result = View(kq);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            NhaTroViewModel nt = new NhaTroViewModel();

            if (id == 0)
            {
                nt.listNhaTro = Repository.GetsList();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", nt) });
            }
            else
            {
                int checkForeign = Repository.CheckForeignKey(id);
                if (checkForeign == 1)
                {
                    int kq = await Repository.Delete(id);
                    if (kq == 0)
                        return NotFound();
                    nt.listNhaTro = Repository.GetsList();
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", nt) });
                }
                else
                {
                    nt.listNhaTro = Repository.GetsList();
                    return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "ViewAll", nt) });
                }
            }
        }
    }
}
