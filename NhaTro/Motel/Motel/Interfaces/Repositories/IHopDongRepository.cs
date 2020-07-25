using Motel.Models;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IHopDongRepository
    {
        IEnumerable<HopDongViewModel> Gets(int idNhaTro);

        Task<HopDong> GetById(int id);

        Task<int> Create(HopDong hd);

        Task<int> Update(HopDong hd);
        Task<int> Delete(int id);

        HopDong GetByIdHD(int id);
        HopDongViewModel GetByIDHopDong(int idNhaTro, int idHopDong);
    }
}
