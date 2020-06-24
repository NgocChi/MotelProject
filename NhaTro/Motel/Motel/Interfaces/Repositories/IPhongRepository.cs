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
        IEnumerable<PhongViewModel> Gets();
        IEnumerable<Phong> GetsPhongTrong();

        IEnumerable<LoaiPhong> GetsLoaiPhong();

        IEnumerable<TrangThaiPhong> GetsTrangThaiPhong();
        Task<Phong> GetById(int id);
        Task<LoaiPhong> GetLoaiPhById(int id);

        Task<int> Create(Phong phong);

        Task<int> Update(Phong phong);

        Task<int> UpdateTTP(int maph, int ttph);

        Task<int> CreateLoaiPhong(LoaiPhong loaiPhong);

        Task<int> UpdateLoaiPhong(LoaiPhong loaiPhong);

        Task<int> Delete(int id);

        Task<int> DeleteLoaiPh(int id);
    }
}
