using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class PhongViewModel : Phong
    {

        public string TenNhaTro { get; set; }

        public string TrangThai { get; set; }

        public int? MaTrangThai { get; set; }

        public string TenLoaiPhong { get; set; }

        public int DienTich { get; set; }

        public decimal Gia { get; set; }

        public decimal GiaDatCoc { get; set; }

        public int SoPhongTrenTang { get; set; }
    }
}
