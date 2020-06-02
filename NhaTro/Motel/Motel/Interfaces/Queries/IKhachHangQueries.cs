using Common;
using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Queries
{
    public interface IKhachHangQueries : IBaseQueries
    {
        Task<IEnumerable<KhachHang>> Gets();
        Task<KhachHang> Get(int id);
    }
}
