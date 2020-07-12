using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class DonViTinhViewModel
    {
        public IEnumerable<DonViTinh> listDonViTinh { get; set; }
        public DonViTinh donViTinh { get; set; }
    }
}
