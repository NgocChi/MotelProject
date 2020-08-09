using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;
using Rotativa.AspNetCore;
using Web;

namespace Motel.Controllers
{
    public class HopDongController : Controller
    {
        private readonly IHopDongRepository Repository = null;
        private readonly IPhongRepository PhongRepository = null;
        private readonly IKhachHangRepository KhachHangRepository = null;
        private readonly IDichVuRepository DichVuRepository = null;
        private readonly IChuTroRepository ChuTroRepository = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDichVuPhongRepository DichVuPhongRepository = null;
        private readonly IDatPhongRepository DatPhongRepository = null;
        private readonly ITaiKhoanRepository TaiKhoanRepository = null;
        private int _nhaTro = 0;
        private string _taikhoan = string.Empty;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;

        public HopDongController(ITaiKhoanRepository taiKhoanRepository, IPhanQuyenRepository phanQuyenRepository, IHttpContextAccessor httpContextAccessor, IDatPhongRepository datPhongRepository, IDichVuPhongRepository dichVuPhongRepository, IChuTroRepository chuTroRepository, IHopDongRepository repository, IPhongRepository phongRepository, IKhachHangRepository khachHangRepository, IDichVuRepository dichVuRepository)
        {
            this.Repository = repository;
            this.TaiKhoanRepository = taiKhoanRepository;
            this.PhongRepository = phongRepository;
            this.KhachHangRepository = khachHangRepository;
            this.DichVuRepository = dichVuRepository;
            this._httpContextAccessor = httpContextAccessor;
            this.ChuTroRepository = chuTroRepository;
            this.DichVuPhongRepository = dichVuPhongRepository;
            this.DatPhongRepository = datPhongRepository;
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("MotelData");
            this.PhanQuyenRepository = phanQuyenRepository;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");
        }
        public IActionResult Index1(int trangThai = 0)
        {
            CommonViewModel common = new CommonViewModel();
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            switch (trangThai)
            {
                case 0:
                    common.qlHopDongViewModel.listHopDong = Repository.Gets(_nhaTro);
                    break;
                case 1:
                    common.qlHopDongViewModel.listHopDong = Repository.Gets(_nhaTro).Where(t => t.NgayKetThuc >= DateTime.Now);
                    break;
                case 2:
                    common.qlHopDongViewModel.listHopDong = Repository.Gets(_nhaTro).Where(t => t.NgayKetThuc < DateTime.Now);
                    break;

            }
            return Json(new { html = Helper.RenderRazorViewToString(this, "Table", common) });
        }


        public IActionResult Index()
        {
            CommonViewModel common = new CommonViewModel();
            common.qlHopDongViewModel.listHopDong = Repository.Gets(_nhaTro);
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            return View(common);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, int idDatPhong, QuanLyHopDongViewModel hopdong)
        {
            int kq = -1;
            try
            {
                if (id == 0)
                {
                    hopdong.hopDongKhachHangPhong.taikhoanKH.LoaiTaiKhoan = false;
                    string tentk = TaiKhoanRepository.CreateTKKhachHang(hopdong.hopDongKhachHangPhong.taikhoanKH);

                    KhachHang khachHang = new KhachHang();
                    khachHang.MaKh = hopdong.hopDongKhachHangPhong.hopDong._MaKH;
                    khachHang.TenTaiKhoan = tentk;
                    await KhachHangRepository.UpdateTKhoan(khachHang);
                    //
                    hopdong.hopDongKhachHangPhong.hopDong.TrangThaiHD = true;
                    kq = await Repository.Create(hopdong.hopDongKhachHangPhong.hopDong);
                    foreach (var item in hopdong.listDichVu)
                    {
                        if (item.IsCheck == true || item.MacDinh == true)
                        {
                            DichVuPhong dvp = new DichVuPhong();
                            dvp._MaDV = item.MaDV;
                            dvp._MaPH = hopdong.hopDongKhachHangPhong.hopDong._MaPH;
                            dvp._MaHD = kq;
                            dvp.SoLuong = (item.SoLuong == null || item.SoLuong == 0) ? 1 : item.SoLuong;
                            await DichVuPhongRepository.Create(dvp);
                        }
                    }

                    //
                    await PhongRepository.UpdateTTP(hopdong.hopDongKhachHangPhong.hopDong._MaPH, 3);
                    await DatPhongRepository.Delete(idDatPhong);
                }
                else
                {
                    hopdong.hopDongKhachHangPhong.hopDong.MaHopDong = id;
                    kq = await Repository.Update(hopdong.hopDongKhachHangPhong.hopDong);
                    foreach (var item in hopdong.listDichVu)
                    {
                        if (item.IsCheck == true || item.MacDinh == true)
                        {
                            DichVuPhong dvp = new DichVuPhong();
                            dvp._MaDV = item.MaDV;
                            dvp._MaPH = hopdong.hopDongKhachHangPhong.hopDong._MaPH;
                            dvp._MaHD = id;
                            dvp.SoLuong = (item.SoLuong == null || item.SoLuong == 0) ? 1 : item.SoLuong;
                            int check = DichVuPhongRepository.CheckExist(dvp);
                            if (check == 1)
                                await DichVuPhongRepository.Create(dvp);
                            else
                                await DichVuPhongRepository.Update(dvp);
                        }
                        else
                        {
                            DichVuPhong dvp = new DichVuPhong();
                            dvp._MaDV = item.MaDV;
                            dvp._MaPH = hopdong.hopDongKhachHangPhong.hopDong._MaPH;
                            dvp._MaHD = id;
                            dvp.SoLuong = item.SoLuong;
                            int check = DichVuPhongRepository.CheckExist(dvp);
                            if (check == 0)
                                await DichVuPhongRepository.Delete(dvp);
                        }
                    }
                }
                CommonViewModel common = new CommonViewModel();
                common.qlHopDongViewModel.listHopDong = Repository.Gets(_nhaTro);
                common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
            }
            catch
            {
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", hopdong) });
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id, int idDatPhong, int idPhong = 0)
        {
            IActionResult result;
            QuanLyHopDongViewModel hd = new QuanLyHopDongViewModel();

            hd.listKhachHang = KhachHangRepository.Gets().Where(t => t._MaNT == _nhaTro);
            hd.listDichVu = DichVuRepository.GetsByNhaTroDEMO(_nhaTro, id);
            hd.listChuTro = ChuTroRepository.Gets().Where(t => t._TenTaiKhoan == _taikhoan);
            hd.hopDongKhachHangPhong = new HopDongKhachHang();
            hd.hopDongKhachHangPhong.hopDong = new HopDong();
            hd.hopDongKhachHangPhong.dichVuPhong = new DichVuPhong();
            hd.hopDongKhachHangPhong.taikhoanKH = new TaiKhoan();
            if (id == 0) // nếu thêm mới hợp đồng
            {
                if (idDatPhong != 0) // nếu đã đặt phòng
                {
                    hd.hopDongKhachHangPhong.datPhong = new DatPhongViewModel();
                    hd.hopDongKhachHangPhong.datPhong = DatPhongRepository.GetsByIdDP(idDatPhong);
                    hd.listPhong = PhongRepository.GetsPTrong(_nhaTro, hd.hopDongKhachHangPhong.datPhong._MaPH);
                    hd.hopDongKhachHangPhong.hopDong._MaPH = hd.hopDongKhachHangPhong.datPhong._MaPH;
                    hd.hopDongKhachHangPhong.hopDong._MaKH = hd.hopDongKhachHangPhong.datPhong._MaKH;
                    hd.hopDongKhachHangPhong.hopDong.GiaPhong = hd.hopDongKhachHangPhong.datPhong.Gia;
                    hd.hopDongKhachHangPhong.hopDong.TienDatCoc = hd.hopDongKhachHangPhong.datPhong.GiaDatCoc;
                    hd.TongTien = hd.hopDongKhachHangPhong.datPhong.GiaDatCoc + hd.hopDongKhachHangPhong.datPhong.Gia - hd.hopDongKhachHangPhong.datPhong.SoTienCoc;
                }
                else
                {
                    hd.hopDongKhachHangPhong.datPhong = new DatPhongViewModel();
                    hd.listPhong = PhongRepository.GetsPTrong(_nhaTro, 0);
                }

                result = View(hd);
            }
            else // nếu edit hợp đồng
            {
                hd.hopDongKhachHangPhong.datPhong = new DatPhongViewModel();
                hd.listPhong = PhongRepository.GetsPTrong(_nhaTro, idPhong);
                hd.hopDongKhachHangPhong.hopDong = await Repository.GetById(id);
                hd.hopDongKhachHangPhong.taikhoanKH = TaiKhoanRepository.GetByTaiKhoanKH(hd.hopDongKhachHangPhong.hopDong._MaKH);

                if (hd.hopDongKhachHangPhong.hopDong == null)
                    result = NotFound();
                result = View(hd);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CommonViewModel common = new CommonViewModel();
            if (id == 0)
            {
                common.qlHopDongViewModel.listHopDong = Repository.Gets(_nhaTro);
                common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
            }
            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                common.qlHopDongViewModel.listHopDong = Repository.Gets(_nhaTro);
                common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
            }
        }

        public async Task<IActionResult> ExportPDF(int id = 0, int idPhong = 0)
        {

            ExportHopDong hd = new ExportHopDong();
            hd.chuTro = ChuTroRepository.GetByTK(_taikhoan);
            hd.listDichVu = DichVuRepository.GetsByNhaTroDEMO(_nhaTro, id);
            hd.hopDongKhachHangPhong = new HopDongKhachHang();
            hd.hopDongKhachHangPhong.datPhong = new DatPhongViewModel();
            hd.hopDongKhachHangPhong.hopDong = Repository.GetByIDHopDong(_nhaTro, id);
            hd.khachHang = await KhachHangRepository.GetsById(hd.hopDongKhachHangPhong.hopDong._MaKH);
            return new ViewAsPdf("ExportPDF", hd)
            {

            };
        }

    }
}