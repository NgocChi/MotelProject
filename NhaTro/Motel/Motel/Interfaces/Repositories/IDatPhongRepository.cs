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

        IEnumerable<DatPhongViewModel> GetsByMaNhaTro(int id);

        Task<int> Create(DatPhong dp);

        Task<int> Update(DatPhong dp);

        Task<int> Delete(int id);

        Task<DatPhong> GetsById(int? id);

        DatPhongViewModel GetsByIdDP(int id);
    }
}
