using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.ViewModels;
using DAL;
using Motel.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Web;

namespace Motel.Controllers
{
    public class XuatHoaDonController : Controller
    {
        private readonly IHoaDonRepository Repository = null;
        private readonly IHttpContextAccessor _httpContextAccessor = null;

        private int _nhaTro = 0;

        public XuatHoaDonController(IHttpContextAccessor httpContextAccessor, IHoaDonRepository repository)
        {
            this.Repository = repository;
            this._httpContextAccessor = httpContextAccessor;
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("UserData");
        }

        public IActionResult Index1(int trangThai = 0)
        {
            QuanLyHoaDonViewModel hd = new QuanLyHoaDonViewModel();
            //switch (trangThai)
            //{
            //    case 0:
            //        hd.listHopDong = Repository.Gets(_nhaTro);
            //        break;
            //    case 1:
            //        hd.listHopDong = Repository.Gets(_nhaTro).Where(t => t.NgayKetThuc >= DateTime.Now);
            //        break;
            //    case 2:
            //        hd.listHopDong = Repository.Gets(_nhaTro).Where(t => t.NgayKetThuc < DateTime.Now);
            //        break;

            //}
            return Json(new { html = Helper.RenderRazorViewToString(this, "Table", hd) });
        }


        public IActionResult Index()
        {
            QuanLyHoaDonViewModel hd = new QuanLyHoaDonViewModel();
            hd.listXuatHoaDon = Repository.Gets(_nhaTro, DateTime.Now);
            return View(hd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, int idDatPhong, QuanLyHoaDonViewModel hopdong)
        {
            int kq = -1;
            try
            {
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", hopdong) });
            }
            catch
            {
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", hopdong) });
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            QuanLyHoaDonViewModel model = new QuanLyHoaDonViewModel();

            return View();
        }
    }
}