using Motel.Models;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IHoaDonRepository
    {
        IEnumerable<HoaDonViewModel> Gets(int id, DateTime Thang);

        Task<int> Create(HoaDon hd);

        Task<int> Update(HoaDon hd);

        Task<int> CreateCT(ChiTietHoaDon ct);

        Task<int> UpdateCT(ChiTietHoaDon ct);
    }
}
