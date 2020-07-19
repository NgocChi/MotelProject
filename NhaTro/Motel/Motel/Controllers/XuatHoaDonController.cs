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
using Motel.Models;

namespace Motel.Controllers
{
    public class XuatHoaDonController : Controller
    {
        private readonly IHoaDonRepository Repository = null;
        private readonly IHttpContextAccessor _httpContextAccessor = null;
        private readonly IPhongRepository PhongRepository = null;
        private readonly IDienNuocRepository DienNuocRepository = null;
        private readonly IDichVuRepository DichVuRepository = null;
        private int _nhaTro = 0;

        public XuatHoaDonController(IHttpContextAccessor httpContextAccessor, IDichVuRepository dichVuRepository, IDienNuocRepository dienNuocRepository, IHoaDonRepository repository, IPhongRepository phongRepository)
        {
            this.Repository = repository;
            this.DichVuRepository = dichVuRepository;
            this.PhongRepository = phongRepository;
            this.DienNuocRepository = dienNuocRepository;
            this._httpContextAccessor = httpContextAccessor;
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("UserData");
        }

        public IActionResult Index1(DateTime thangNam, int trangThai = 0)
        {
            QuanLyHoaDonViewModel hd = new QuanLyHoaDonViewModel();
            hd.ThangNam = thangNam;
            switch (trangThai)
            {
                case 0:
                    hd.listXuatHoaDon = Repository.Gets(_nhaTro, thangNam);
                    break;
                case 1:
                    hd.listXuatHoaDon = Repository.Gets(_nhaTro, thangNam);
                    break;
                case 2:
                    hd.listXuatHoaDon = Repository.Gets(_nhaTro, thangNam);
                    break;

            }
            return Json(new { html = Helper.RenderRazorViewToString(this, "Table", hd) });
        }


        public IActionResult Index()
        {
            QuanLyHoaDonViewModel hd = new QuanLyHoaDonViewModel();
            hd.ThangNam = DateTime.Now;
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
        public async Task<IActionResult> AddOrEdit(int id, int _MaKhachHang, int _MaPhong, int _MaHopDong, DateTime thangNam)
        {
            IActionResult result;
            QuanLyHoaDonViewModel model = new QuanLyHoaDonViewModel();
            model.phong = PhongRepository.GetByIdPhong(_MaPhong);
            model.dienNuoc = DienNuocRepository.GetDienNuocByIdPhong(_MaPhong, thangNam);
            model.listDichVu = DichVuRepository.GetsByIdPhongIdHopDong(_MaHopDong, _MaPhong);
            model.hoaDon = new HoaDon();
            model.ThangNam = thangNam;

            return View(model);
        }
    }
}