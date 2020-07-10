using Motel.Models;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IPhongRepository
    {
        IEnumerable<PhongViewModel> Gets(int _nhaTro);
        IEnumerable<PhongViewModel> Gets();
        IEnumerable<Phong> GetsPhongTrong();

        IEnumerable<TrangThaiPhong> GetsTrangThaiPhong();
        Task<Phong> GetById(int id);

        Task<int> Create(Phong phong);

        Task<int> Update(Phong phong);

        Task<int> UpdateTTP(int maph, int ttph);

        Task<int> Delete(int id);

    }
}
