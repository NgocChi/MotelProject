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

        int Create(Phong phong);

        int Update(Phong phong);

        int UpdateTTP(int maph, int ttph);

        int CreateLoaiPhong(LoaiPhong loaiPhong);

        int UpdateLoaiPhong(LoaiPhong loaiPhong);
    }
}
