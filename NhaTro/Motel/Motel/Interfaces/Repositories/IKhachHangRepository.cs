using Motel.Models;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IKhachHangRepository
    {
        IEnumerable<KhachHang> Gets();

        Task<int> UpdateTKhoan(KhachHang khach);
        Task<KhachHang> GetsById(int? id);

        Task<int> Create(KhachHang khach);

        Task<int> Update(KhachHang khach);

        Task<int> Delete(int id);

        int CheckForeignKey(int id);

    }
}
