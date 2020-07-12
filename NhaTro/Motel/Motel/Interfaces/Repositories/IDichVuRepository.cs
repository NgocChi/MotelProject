using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IDichVuRepository
    {
        IEnumerable<DichVu> Gets();
        IEnumerable<DichVu> GetsByNhaTro(int id);

        Task<DichVu> GetsById(int? id);
        Task<int> Create(DichVu dv);

        Task<int> Update(DichVu dv);

        Task<int> Delete(int id);
    }
}
