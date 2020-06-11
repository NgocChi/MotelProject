using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class KhachHangViewModel
    {
        public KhachHang khachHang { get; set; }
        public IEnumerable<KhachHang> list { get; set; }
    }
}
