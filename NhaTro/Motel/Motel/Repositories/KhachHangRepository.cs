using Common;
using DAL;
using Dapper;
using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly AppDBContext _appDBContext;

        public KhachHangRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public IEnumerable<KhachHang> Gets()
        {
           
            return _appDBContext.KhachHangs.ToList();
        }


        public async Task<IEnumerable<KhachHang>> _Gets()
        {

            return  _appDBContext.KhachHangs.ToList();
        }

        public int Create(KhachHang kh)
        {
            if (kh != null)
            {
                _appDBContext.KhachHangs.Add(kh);
                _appDBContext.SaveChanges();
                return 1;
            }
            return 0;

        }

    }
}
