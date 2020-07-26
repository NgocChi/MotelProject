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
        int Update(TaiKhoan taiKhoan);

        IEnumerable<TaiKhoan> Gets();

        int CreateChuTro(ChuTro ct);


        ChuTro GetByTaiKhoan(string tk);

        string CreateTKKhachHang(TaiKhoan taikhoan);
    }
}
