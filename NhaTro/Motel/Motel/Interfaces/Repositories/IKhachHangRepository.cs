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

        Task<IEnumerable<KhachHang>> _Gets();

        int Create(KhachHang kh);
    }
}
