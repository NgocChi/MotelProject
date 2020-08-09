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
using Rotativa.AspNetCore;

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
        private readonly IKhachHangRepository KhachHangRepository = null;
        private readonly IChuTroRepository ChuTroRepository = null;
        private string _taikhoan = string.Empty;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;

        public XuatHoaDonController(IKhachHangRepository khachHangRepository, IChuTroRepository chuTroRepository, IPhanQuyenRepository phanQuyenRepository, IHttpContextAccessor httpContextAccessor, IDichVuRepository dichVuRepository, IDienNuocRepository dienNuocRepository, IHoaDonRepository repository, IPhongRepository phongRepository)
        {
            this.Repository = repository;
            this.KhachHangRepository = khachHangRepository;
            this.ChuTroRepository = chuTroRepository;
            this.DichVuRepository = dichVuRepository;
            this.PhongRepository = phongRepository;
            this.DienNuocRepository = dienNuocRepository;
            this._httpContextAccessor = httpContextAccessor;
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("MotelData");
            this.PhanQuyenRepository = phanQuyenRepository;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");
        }

        public IActionResult Index1(int trangThai = 0)
        {
            DateTime tn = DateTime.Parse("1/1/0001 12:00:00 AM");
            DateTime date = _httpContextAccessor.HttpContext.Session.GetComplexData<DateTime>("Date") == tn ? DateTime.Now : _httpContextAccessor.HttpContext.Session.GetComplexData<DateTime>("Date");

            CommonViewModel common = new CommonViewModel();
            common.qlXuatHoaDonViewModel.listXuatHoaDon = Repository.Gets(_nhaTro, DateTime.Now);
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            common.qlXuatHoaDonViewModel.ThangNam = DateTime.Now;
            switch (trangThai)
            {
                case 0:
                    common.qlXuatHoaDonViewModel.listXuatHoaDon = Repository.Gets(_nhaTro, date);
                    break;
                case 1:
                    common.qlXuatHoaDonViewModel.listXuatHoaDon = Repository.Gets(_nhaTro, date).Where(t => t.TonTai == false && t.TrangThai == false);
                    break;
                case 2:
                    common.qlXuatHoaDonViewModel.listXuatHoaDon = Repository.Gets(_nhaTro, date).Where(t => t.TonTai == true && t.TrangThai == false);
                    break;
                case 3:
                    common.qlXuatHoaDonViewModel.listXuatHoaDon = Repository.Gets(_nhaTro, date).Where(t => t.TonTai == true && t.TrangThai == true);
                    break;

            }
            return Json(new { html = Helper.RenderRazorViewToString(this, "Table", common) });
        }


        public IActionResult Index()
        {
            CommonViewModel common = new CommonViewModel();
            common.qlXuatHoaDonViewModel.listXuatHoaDon = Repository.Gets(_nhaTro, DateTime.Now);
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            common.qlXuatHoaDonViewModel.ThangNam = DateTime.Now;
            return View(common);
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
                    hoadon.hoaDon.TrangThai = false;
                    hoadon.hoaDon._MaLoaiHD = 1;
                    kq = await Repository.Create(hoadon.hoaDon);
                    await DienNuocRepository.UpdateChotSo(hoadon.dienNuoc.MaDienNuoc);
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
                CommonViewModel common = new CommonViewModel();
                common.qlXuatHoaDonViewModel.listXuatHoaDon = Repository.Gets(_nhaTro, DateTime.Now);
                common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                common.qlXuatHoaDonViewModel.ThangNam = DateTime.Now;
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
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
            model.Key = id == 0 ? "add" : "edit";
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

        public async Task<IActionResult> ExportPDF(int id, int _MaKhachHang, int _MaPhong, int _MaHopDong, DateTime thangNam)
        {
            ExportXuatHoaDon xhd = new ExportXuatHoaDon();
            xhd.chuTro = ChuTroRepository.GetByTK(_taikhoan);
            xhd.hoaDon = new HoaDon();
            xhd.hoaDon._MaHD = _MaHopDong;
            xhd.hoaDon._MaPH = _MaPhong;
            xhd.ThangNam = thangNam;
            xhd.phong = PhongRepository.GetByIdPhong(_MaPhong);
            xhd.dienNuoc = DienNuocRepository.GetDienNuocByIdPhong(_MaPhong, thangNam);
            xhd.listDichVu = DichVuRepository.GetsByIdPhongIdHopDong(_MaHopDong, _MaPhong);
            xhd.TongTienDienNuoc = 0;
            xhd.TongTienDichVu = 0;
            xhd.ThanhTienHoaDon = 0;
            xhd.khachHang = await KhachHangRepository.GetsById(_MaKhachHang);
            foreach (var item in xhd.listDichVu)
            {
                if (item.Ten == "Điện")
                {
                    item.ThanhTien = item.Gia * xhd.dienNuoc.TieuThuDien;
                    xhd.TongTienDienNuoc += item.ThanhTien;
                }
                else if (item.Ten == "Nước")
                {
                    item.ThanhTien = item.Gia * xhd.dienNuoc.TieuThuNuoc;
                    xhd.TongTienDienNuoc += item.ThanhTien;
                }
                else
                {
                    item.ThanhTien = item.Gia * item.SoLuong;
                    xhd.TongTienDichVu += item.ThanhTien;
                }
            }


            xhd.ThanhTienHoaDon = xhd.TongTienDichVu + xhd.TongTienDienNuoc + xhd.phong.Gia;

            return new ViewAsPdf("ExportPDF", xhd)
            {

            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThanhToan(int id)
        {
            await Repository.UpdateThanhToan(id);
            CommonViewModel common = new CommonViewModel();
            common.qlXuatHoaDonViewModel.listXuatHoaDon = Repository.Gets(_nhaTro, DateTime.Now);
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            common.qlXuatHoaDonViewModel.ThangNam = DateTime.Now;
            return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "Table", common) });
        }

        [HttpGet]
        public IActionResult ChangeDate(DateTime thangNam)
        {

            _httpContextAccessor.HttpContext.Session.SetComplexData("Date", thangNam);
            CommonViewModel common = new CommonViewModel();
            common.qlXuatHoaDonViewModel.listXuatHoaDon = Repository.Gets(_nhaTro, DateTime.Now);
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            return View(common);

        }
    }
}