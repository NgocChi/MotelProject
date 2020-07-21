using Motel.Models;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IPhanQuyenRepository
    {
        List<PhanQuyenViewModel> GetsManHinh(int idMaNND);

        int CheckForeignKey(int idMaNND, int idMaMH);

        Task<int> Create(PhanQuyen pquyen);

        Task<int> Update(PhanQuyen pquyen);
    }
}
