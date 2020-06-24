using Motel.Models;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface INhaTroRepository
    {
        IEnumerable<NhaTro> Gets();

        Task<NhaTro> GetsById(int? id);

        Task<int> Create(NhaTro nhaTro);

        Task<int> Update(NhaTro nhaTro);

        Task<int> Delete(int id);

        Task<int> UpdateSoLuongPhong(int maNt, int soluong);

    }
}
