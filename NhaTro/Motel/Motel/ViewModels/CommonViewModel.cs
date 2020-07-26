using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class CommonViewModel
    {

        public List<ManHinh> list { set; get; } = new List<ManHinh>();

        public NhaTroViewModel nhaTroViewModel = new NhaTroViewModel();

        public QuanLyLoaiPhongViewModel qlLoaiPhongViewModel = new QuanLyLoaiPhongViewModel();

        public QuanLyPhongViewModel qlPhongViewModel = new QuanLyPhongViewModel();

        public QuanLyNhomNguoiDungViewModel qlPhanQuyenViewModel = new QuanLyNhomNguoiDungViewModel();

        public QuanLyHopDongViewModel qlHopDongViewModel = new QuanLyHopDongViewModel();

        public QuanLyDatPhongViewModel qlDatPhongViewModel = new QuanLyDatPhongViewModel();

        public LoaiDichVuViewModel loaiDVViewModel = new LoaiDichVuViewModel();

        public DichVuViewModel dichVuViewModel = new DichVuViewModel();

        public KhachHangViewModel qlKhachHangViewModel = new KhachHangViewModel();

        public DonViTinhViewModel donViTinhViewModel = new DonViTinhViewModel();

        public QuanLyHoaDonViewModel qlXuatHoaDonViewModel = new QuanLyHoaDonViewModel();

        public HomeViewModel homeViewModel = new HomeViewModel();

        public QuanLyDienNuocViewModel qlDienNuocViewModel = new QuanLyDienNuocViewModel();

    }

    public class ExportFilePDF
    {
        public NhaTroViewModel nhaTroViewModel = new NhaTroViewModel();
    }

    public class ExportDatCocPhong
    {
        public ChuTro chuTro { get; set; }

        public DatPhongViewModel datPhong { get; set; }
    }

    public class ExportHopDong
    {
        public ChuTro chuTro { get; set; }

        public DatPhongViewModel datPhong { get; set; }
    }

    public class ExportXuatHoaDon
    {
        public ChuTro chuTro { get; set; }

        public List<DichVu_ViewModel> listDichVu { get; set; }

        public HopDongKhachHang hopDongKhachHangPhong { get; set; }

        public decimal TongTien { get; set; }
    }
}
