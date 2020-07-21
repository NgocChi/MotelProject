using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class NhaTroViewModel
    {
        public IEnumerable<NhaTro> listNhaTro { get; set; }
        public NhaTro nhaTro { get; set; }

        public bool choose { get; set; }

    }
}
