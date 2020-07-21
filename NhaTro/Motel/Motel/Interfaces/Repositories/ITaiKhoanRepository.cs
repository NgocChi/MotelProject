using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface ITaiKhoanRepository
    {
        TaiKhoan DangNhap(string userName, string password);
        int Create(TaiKhoan taiKhoan);

        IEnumerable<TaiKhoan> Gets();

        int GetIdUserByTenTaiKhoan(string tenTaiKhoan, bool loaiTaiKhoan);
        int CreateChuTro(ChuTro ct);

        ChuTro GetByTaiKhoan(string tk);
    }
}
