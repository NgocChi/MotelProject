using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface ILoaiPhongRepository
    {
        Task<int> DeleteLoaiPh(int id);
        Task<int> CreateLoaiPhong(LoaiPhong loaiPhong);

        Task<int> UpdateLoaiPhong(LoaiPhong loaiPhong);
        IEnumerable<LoaiPhong> GetsLoaiPhong();
        Task<LoaiPhong> GetLoaiPhById(int id);
        int CheckForeignKey(int id);
    }
}
