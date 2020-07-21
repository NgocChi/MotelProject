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
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("MotelData");
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
        public async Task<IActionResult> AddOrEdit(int id, int maHDong, int maPhong, QuanLyHoaDonViewModel hoadon)
        {
            int kq = -1;
            try
            {
                if (id == 0)
                {
                    hoadon.hoaDon = new HoaDon();
                    hoadon.hoaDon.NgayLap = hoadon.ThangNam;
                    hoadon.hoaDon.ThanhTien = hoadon.ThanhTienHoaDon;
                    hoadon.hoaDon._MaHD = maHDong;
                    hoadon.hoaDon._MaPH = maPhong;
                    kq = await Repository.Create(hoadon.hoaDon);
                    foreach (var item in hoadon.listDichVu)
                    {
                        ChiTietHoaDon ct = new ChiTietHoaDon();
                        ct._MaDVP = item._MaDichVuPhong;
                        ct.ThanhTien = item.ThanhTien;
                        ct._MaHoaDon = kq;
                        await Repository.CreateCT(ct);
                    }
                }
                else
                {

                }
                QuanLyHoaDonViewModel hd = new QuanLyHoaDonViewModel();
                hd.ThangNam = DateTime.Now;
                hd.listXuatHoaDon = Repository.Gets(_nhaTro, DateTime.Now);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", hd) });
            }
            catch
            {
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", hoadon) });
            }

        }

        [HttpGet]
        public IActionResult AddOrEdit(int id, int _MaKhachHang, int _MaPhong, int _MaHopDong, DateTime thangNam)
        {
            QuanLyHoaDonViewModel model = new QuanLyHoaDonViewModel();
            model.ThangNam = thangNam;
            model.hoaDon = new HoaDon();
            model.hoaDon._MaHD = _MaHopDong;
            model.hoaDon._MaPH = _MaPhong;
            model.phong = PhongRepository.GetByIdPhong(_MaPhong);
            model.dienNuoc = DienNuocRepository.GetDienNuocByIdPhong(_MaPhong, thangNam);
            model.listDichVu = DichVuRepository.GetsByIdPhongIdHopDong(_MaHopDong, _MaPhong);
            model.TongTienDienNuoc = 0;
            model.TongTienDichVu = 0;
            model.ThanhTienHoaDon = 0;
            foreach (var item in model.listDichVu)
            {
                if (item.Ten == "Điện")
                {
                    item.ThanhTien = item.Gia * model.dienNuoc.TieuThuDien;
                    model.TongTienDienNuoc += item.ThanhTien;
                }
                else if (item.Ten == "Nước")
                {
                    item.ThanhTien = item.Gia * model.dienNuoc.TieuThuNuoc;
                    model.TongTienDienNuoc += item.ThanhTien;
                }
                else
                {
                    item.ThanhTien = item.Gia * item.SoLuong;
                    model.TongTienDichVu += item.ThanhTien;
                }
            }


            model.ThanhTienHoaDon = model.TongTienDichVu + model.TongTienDienNuoc + model.phong.Gia;
            return View(model);
        }
    }
}