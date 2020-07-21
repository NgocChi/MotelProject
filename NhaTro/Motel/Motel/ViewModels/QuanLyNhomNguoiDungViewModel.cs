using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class QuanLyNhomNguoiDungViewModel
    {
        public IEnumerable<NhomNguoiDung> listNhomNguoiDung { get; set; }
        public List<PhanQuyenViewModel> listPhanQuyen { get; set; }
    }

    public class PhanQuyenViewModel : PhanQuyen
    {
        public string TenManHinh { get; set; }

        public string KeyControl { get; set; }
    }
}
