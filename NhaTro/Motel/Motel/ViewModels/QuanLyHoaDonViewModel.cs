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
    }

    public class HoaDonViewModel
    {
        public string TenPhong { get; set; }

        public DateTime ThangNam { get; set; }

        public string TenKhachHang { get; set; }

        public string LoaiHoaDon { get; set; }

        public string TenTrangThai { get; set; }

        public bool TrangThai { get; set; }

        public bool TonTai { get; set; }

    }
}
