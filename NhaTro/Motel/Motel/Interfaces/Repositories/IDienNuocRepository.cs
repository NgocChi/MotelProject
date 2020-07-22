using Motel.Models;
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

        IEnumerable<DienNuocViewModel> Gets();

        Task<int> Create(DienNuoc dn);

        Task<int> Update(DienNuoc dn);

        Task<DienNuoc> GetById(int id);
    }
}
