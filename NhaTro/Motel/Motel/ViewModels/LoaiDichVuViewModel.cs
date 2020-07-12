using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class LoaiDichVuViewModel
    {
        public IEnumerable<LoaiDichVuView> listLoaiDichVu { get; set; }

        public IEnumerable<DonViTinh> listDonViTinh { get; set; }
        public LoaiDichVu loaiDichVu { get; set; }
    }

    public class LoaiDichVuView : LoaiDichVu
    {
        public string TenDonViTinh { get; set; }
        public bool IsCheck { get; set; }
    }
}
