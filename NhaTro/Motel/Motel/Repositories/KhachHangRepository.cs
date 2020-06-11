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

        public KhachHangViewModel Gets()
        {
            var kq = new KhachHangViewModel();
            List<KhachHang> list = new List<KhachHang>();
            list = _appDBContext.KhachHangs.ToList();
            kq.list = list;
            return kq;
        }

        public void Save(KhachHang kh)
        {
            _appDBContext.KhachHangs.Add(kh);
            _appDBContext.SaveChanges();
        }
        
    }
}
