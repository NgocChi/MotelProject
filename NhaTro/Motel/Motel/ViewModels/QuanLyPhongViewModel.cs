using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class QuanLyPhongViewModel
    {
        public IEnumerable<PhongViewModel> listPhong { get; set; }

        public IEnumerable<LoaiPhong> listLoaiPhong { get; set; }

        public IEnumerable<NhaTro> listNhaTro { get; set; }

        public IEnumerable<TrangThaiPhong> listTrangThaiPhong { get; set; }

        public Phong phong { get; set; }

        public PhongViewModel phongViewModel { get; set; }


    }
}
