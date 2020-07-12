using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IDonViTinhRepository
    {
        IEnumerable<DonViTinh> Gets();
        Task<DonViTinh> GetsById(int? id);
        Task<int> Create(DonViTinh dvt);

        Task<int> Update(DonViTinh dvt);

        Task<int> Delete(int id);
    }
}
