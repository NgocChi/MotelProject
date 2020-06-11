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
           return  _appDBContext.TaiKhoans.FirstOrDefault(p => (p.TenTaiKhoan == userName & p.MatKhau == password));
        }
    }
}
