using Motel.Models;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface ILoaiDichVuRepository
    {
        IEnumerable<LoaiDichVuView> Gets();
        List<LoaiDichVuViewModel1> GetList();

        Task<LoaiDichVu> GetsById(int? id);
        Task<int> Create(LoaiDichVu loai);

        Task<int> Update(LoaiDichVu loai);

        Task<int> Delete(int id);
    }
}
