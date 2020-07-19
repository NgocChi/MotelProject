using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class QuanLyHoaDonViewModel
    {
        public IEnumerable<HoaDonViewModel> listXuatHoaDon { get; set; }

        public List<DichVu_ViewModel> listDichVu { get; set; }

        public PhongViewModel phong { get; set; }

        public HoaDon hoaDon { get; set; }

        public ChiTietHoaDon ctHoaDon { get; set; }

        public DienNuocViewModel dienNuoc { get; set; }

        public DateTime ThangNam { get; set; }

        public decimal? TongTienDienNuoc { get; set; }
        public decimal? TongTienDichVu { get; set; }

        public decimal? ThanhTienHoaDon { get; set; }


    }

    public class HoaDonViewModel
    {
        public string TenPhong { get; set; }

        public int _MaPhong { get; set; }

        public DateTime ThangNam { get; set; }

        public string TenKhachHang { get; set; }

        public int _MaKhachHang { get; set; }

        public int _MaHopDong { get; set; }

        public string LoaiHoaDon { get; set; }

        public string TenTrangThai { get; set; }

        public bool TrangThai { get; set; }

        public bool TonTai { get; set; }

    }
}
