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

        public ViewResult Index(int? id)
        {
           
            nt.listNhaTro = Repository.Gets();
            return View(nt);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int? id, [Bind("Ten", "DiaChi", "TongPhong", "PhongTrong", "Mota")] NhaTroViewModel nhaTroViewModel)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    kq = await Repository.Create(nhaTroViewModel.nhaTro);
                }
                else
                {
                    try
                    {
                        kq = await Repository.Update(nhaTroViewModel.nhaTro);
                    }
                    catch
                    {
                        throw;
                    }
                }
                return Json(new { IsValid = true, htnl = Helper.RenderRazorViewToString(this, "ViewAll", nt) });
            }
            return Json(new { IsValid = false, htnl = Helper.RenderRazorViewToString(this, "AddOrEdit", nhaTroViewModel) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? id )
        {
           
            if (id == 0)
                return View(new NhaTroViewModel());
            else
            {
                var kq = await Repository.GetsById(id);
                if (kq == null)
                    return NotFound();
                return View(kq);

            }
        }

        [HttpPost]
        public  IActionResult Delete(int id=0)
        {
            if(id == 0)
                return View(new NhaTroViewModel());
            else
            {
                var kq  =  Repository.GetsById(id);
                if (kq == null)
                    return NotFound();
                return View(kq);

            }
        }

    }
}