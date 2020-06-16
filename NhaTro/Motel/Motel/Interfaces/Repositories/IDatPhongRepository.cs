using Motel.Models;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IDatPhongRepository
    {
        IEnumerable<DatPhongViewModel> Gets();

        int Create(DatPhong dp);

        int Update(DatPhong dp);
    }
}
