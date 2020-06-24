using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class QuanLyDatPhongViewModel
    {
        public IEnumerable<Phong> listPhong { get; set; }

        public IEnumerable<DatPhongViewModel> listDatPhong { get; set; }

        public IEnumerable<KhachHang> listKhachHang { get; set; }

        public KhachHangDatPhongViewModel khachHangDatPhong { get; set; }

    }

    public class KhachHangDatPhongViewModel
    {
        public DatPhong datPhong { get; set; }
        public KhachHang khachHang { get; set; }
    }

    public class DatPhongViewModel : DatPhong
    {
        public string TenPhong { get; set; }

        public string TenKhachHang { get; set; }

        public string SoDienThoai { get; set; }


    }
}
