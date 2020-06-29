using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.Queries;
using Motel.Repositories;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controls;

namespace Motel.Controllers
{

    public class KhachHangController : Controller
    {
        private readonly IKhachHangRepository Repository = null;
        public KhachHang khach;


        public KhachHangController(IKhachHangRepository queries)
        {
            this.Repository = queries;
        }

        public ViewResult Index()
        {
            KhachHangViewModel kh = new KhachHangViewModel();
            kh.list = Repository.Gets();
            return View(kh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, KhachHangViewModel ViewModel)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    kq = await Repository.Create(ViewModel.khachHang);
                }
                else
                {
                    try
                    {
                        ViewModel.khachHang.MaKh = id;
                        kq = await Repository.Update(ViewModel.khachHang);
                    }
                    catch
                    {
                        throw;
                    }
                }
                KhachHangViewModel kh = new KhachHangViewModel();
                kh.list = Repository.Gets();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", kh) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", ViewModel) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            KhachHangViewModel kh = new KhachHangViewModel();
            if (id == 0)
            {
                kh.khachHang = new KhachHang();
                return View(kh);
            }
            else
            {
                kh.khachHang = await Repository.GetsById(id);
                if (kh.khachHang == null)
                    result = NotFound();
                result = View(kh);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            KhachHangViewModel kh = new KhachHangViewModel();
            if (id == 0)
            {
                kh.list = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", kh) });
            }
            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                kh.list = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", kh) });
            }
        }






    }
}
