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
            nt.listNhaTro = Repository.Gets();
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
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "Index", Repository.Gets()) });
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
        public IActionResult Delete(int id = 0)
        {
            if (id == 0)
                return View(new NhaTroViewModel());
            else
            {
                var kq = Repository.GetsById(id);
                if (kq == null)
                    return NotFound();
                return View(kq);

            }
        }

    }
}
