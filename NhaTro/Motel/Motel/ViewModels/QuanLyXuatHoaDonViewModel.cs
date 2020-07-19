using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class QuanLyXuatHoaDonViewModel
    {
        public IEnumerable<HopDongViewModel> listHopDong { get; set; }
        public IEnumerable<PhongViewModel> listPhong { get; set; }


    }
}
