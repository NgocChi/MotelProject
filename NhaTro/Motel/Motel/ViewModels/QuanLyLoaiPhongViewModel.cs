using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class QuanLyLoaiPhongViewModel
    {
        public IEnumerable<LoaiPhong> listLoaiPhong { get; set; }
        public LoaiPhong loaiPhong { get; set; }
    }
}
