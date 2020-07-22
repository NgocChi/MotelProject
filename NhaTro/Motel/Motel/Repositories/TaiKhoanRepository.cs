using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Motel.Repositories
{
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly AppDBContext _appDBContext;

        public TaiKhoanRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public TaiKhoan DangNhap(string userName, string password)
        {
            return _appDBContext.TaiKhoans.FirstOrDefault(p => (p.TenTaiKhoan == userName & p.MatKhau == password));
        }
        public int Create(TaiKhoan taikhoan)
        {

            var find = _appDBContext.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == taikhoan.TenTaiKhoan);
            if (find == null)
            {
                _appDBContext.TaiKhoans.Add(taikhoan);
                _appDBContext.SaveChanges();
                return 1;
            }
            return 0;

        }

        public int Update(TaiKhoan taikhoan)
        {

            var find = _appDBContext.TaiKhoans.FirstOrDefault(p => p.TenTaiKhoan == taikhoan.TenTaiKhoan);
            if (find != null)
            {
                find._MaNND = taikhoan._MaNND;
                _appDBContext.TaiKhoans.Update(taikhoan);
                _appDBContext.SaveChanges();
                return 1;
            }
            return 0;

        }




        public int CreateChuTro(ChuTro ct)
        {
            if (ct != null)
            {
                _appDBContext.ChuTros.Add(ct);
                _appDBContext.SaveChanges();
                return 1;
            }
            return 0;
        }

        public IEnumerable<TaiKhoan> Gets()
        {
            return _appDBContext.TaiKhoans.ToList();
        }

        public ChuTro GetByTaiKhoan(string tk)
        {
            return _appDBContext.ChuTros.Where(t => t._TenTaiKhoan == tk).FirstOrDefault();
        }


    }
}
