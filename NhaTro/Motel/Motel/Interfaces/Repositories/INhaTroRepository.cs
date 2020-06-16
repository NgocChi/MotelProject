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
        int Create(NhaTro nhaTro);

        int Update(NhaTro nhaTro);

        int UpdateSoLuongPhong(int maNt, int soluong);

    }
}
