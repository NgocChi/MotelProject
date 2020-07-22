using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class QuanLyLoaiPhongViewModel
    {
        public List<ManHinh> listManHinh { get; set; }
        public IEnumerable<LoaiPhong> listLoaiPhong { get; set; }
        public LoaiPhong loaiPhong { get; set; }
    }
}
