using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class QuanLyHopDongViewModel
    {
        public IEnumerable<HopDongViewModel> listHopDong { get; set; }
        public IEnumerable<PhongViewModel> listPhong { get; set; }

        public IEnumerable<ChuTro> listChuTro { get; set; }

        public IEnumerable<KhachHang> listKhachHang { get; set; }

        public List<DichVu_ViewModel> listDichVu { get; set; }

        public IEnumerable<KhachHang> listKHDestination { get; set; }

        public IEnumerable<DichVu> listDichVuDestination { get; set; }

        public HopDongKhachHang hopDongKhachHangPhong { get; set; }

    }

    public class HopDongViewModel : HopDong
    {
        public string TenPhong { get; set; }

        public string TenKhachHang { get; set; }

        public int ThoiHanHopDong { get; set; }
    }

    public class HopDongKhachHang
    {
        public DatPhong datPhong { get; set; }

        public HopDong hopDong { get; set; }

        public DichVuPhong dichVuPhong { get; set; }

    }
}
