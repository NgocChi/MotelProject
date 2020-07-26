using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class DienNuocViewModel
    {
        public int MaDienNuoc { get; set; }

        public int CSMoiDien { get; set; }
        public int? CSCuDien { get; set; }

        public int CSMoiNuoc { get; set; }
        public int? CSCuNuoc { get; set; }

        public DateTime NgayGhi { get; set; }

        public int MaPhong { get; set; }

        public int? TieuThuDien { get; set; }

        public int? TieuThuNuoc { get; set; }
    }
}
