using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class DichVuViewModel
    {
        public IEnumerable<DichVu_ViewModel> listDichVu { get; set; }

        public IEnumerable<NhaTro> listNhaTro { get; set; }
        public List<LoaiDichVuView> listLoaiDichVu { get; set; }

        public IEnumerable<DonViTinh> listDonViTinh { get; set; }
        public DichVu dichVu { get; set; }
    }

    public class DichVu_ViewModel : DichVu
    {
        public string TenDonVi { get; set; }
        public string TenNhaTro { get; set; }

        public bool IsCheck { get; set; }

        public int? SoLuong { get; set; }

        public int _MaDichVuPhong { get; set; }


    }

}
