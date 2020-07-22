using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class QuanLyDienNuocViewModel
    {
        public IEnumerable<DienNuocViewModel> listDienNuoc { get; set; }

        public IEnumerable<PhongViewModel> listPhong { get; set; }

        public DienNuoc dienNuoc { get; set; }
    }
}
