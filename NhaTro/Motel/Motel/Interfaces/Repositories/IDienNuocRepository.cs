using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IDienNuocRepository
    {
        DienNuocViewModel GetDienNuocByIdPhong(int idPhong, DateTime ngayghi);
    }
}
