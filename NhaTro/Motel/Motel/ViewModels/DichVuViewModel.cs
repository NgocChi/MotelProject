using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class DichVuViewModel
    {
        public IEnumerable<DichVu> listDichVu { get; set; }

        public IEnumerable<NhaTro> listNhaTro { get; set; }
        public List<LoaiDichVuViewModel1> listLoaiDichVu { get; set; }

        public IEnumerable<DonViTinh> listDonViTinh { get; set; }
        public DichVu dichVu { get; set; }
    }

    public class LoaiDichVuViewModel1
    {
        public bool IsCheck { get; set; }

        public int MaLoaiDV { get; set; }

        public string TenLoaiDV { get; set; }
    }

}
