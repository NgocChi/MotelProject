using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;

namespace Motel.Controllers
{
    public class NhaTroController : Controller
    {
        private readonly INhaTroRepository Repository = null;
        
        public NhaTroController(INhaTroRepository repository)
        {
            this.Repository = repository;
        }

        public ViewResult Index(int? id)
        {
            NhaTroViewModel nt = new NhaTroViewModel();
            nt.listNhaTro = Repository.Gets();
            if(id != 0)
            {
                nt.nhaTro = Repository.GetsById(id);
            }
            return View(nt);
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            NhaTroViewModel nt = new NhaTroViewModel();

            return View(nt);
        }

        [HttpPost]
        public IActionResult Create(NhaTro nhaTro)
        {
            int kq = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["info"] = "Dữ liệu bị trống";
                    return RedirectToAction(nameof(NhaTroController.Create), "NhaTro");
                }
                kq = Repository.Create(nhaTro);
                if (kq == 1)
                {
                    ViewData["info"] = "Thêm nhà trọ thành công";
                    return RedirectToAction(nameof(NhaTroController.Index), "NhaTro");
                }
                else
                {
                    ViewData["info"] = "Thêm thất bại";
                    return RedirectToAction(nameof(NhaTroController.Create), "NhaTro");
                }

            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return RedirectToAction(nameof(NhaTroController.Create), "NhaTro");
            }

        }
        [HttpPost]
        public IActionResult Update(NhaTro nhaTro)
        {
            int kq = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["info"] = "Dữ liệu bị trống";
                    return RedirectToAction(nameof(NhaTroController.Update), "NhaTro");
                }
                kq = Repository.Update(nhaTro);
                if (kq == 1)
                {
                    ViewData["info"] = "Cập nhật nhà trọ thành công";
                    return RedirectToAction(nameof(NhaTroController.Index), "NhaTro");
                }
                else
                {
                    ViewData["info"] = "Cập nhật thất bại";
                    return RedirectToAction(nameof(NhaTroController.Update), "NhaTro");
                }

            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return RedirectToAction(nameof(NhaTroController.Update), "NhaTro");
            }

        }
    }
}