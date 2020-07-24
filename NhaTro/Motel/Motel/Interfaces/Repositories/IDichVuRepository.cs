using Motel.Models;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IDichVuRepository
    {
        IEnumerable<DichVu> Gets();
        List<DichVu_ViewModel> GetsByNhaTro(int id, int idPhong);
        List<DichVu_ViewModel> GetsByNhaTroDEMO(int id, int idHopDong);
        List<DichVu_ViewModel> GetsByNhaTro(int id);

        List<DichVu_ViewModel> GetsByIdPhongIdHopDong(int idHopDong, int idPhong);

        Task<DichVu> GetsById(int? id);
        Task<int> Create(DichVu dv);

        Task<int> Update(DichVu dv);

        Task<int> Delete(int id);

        LoaiDichVuView GetsByIdMaLoaiDV(int? id);

        int CheckForeignKey(int id);
    }
}
