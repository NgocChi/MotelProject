using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class PhongViewModel: Phong
    {
       public IEnumerable<Phong> list { get; set; }
    }
}
