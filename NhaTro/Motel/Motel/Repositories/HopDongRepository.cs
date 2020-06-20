using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class HopDongRepository: IHopDongRepository
    {
        private readonly AppDBContext _appDBContext;

        public HopDongRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public IEnumerable<HopDongViewModel> Gets()
        {
            var query = from hd in _appDBContext.HopDongs
                        join p in _appDBContext.Phongs on hd._MaPH equals p.MaPH
                        join kh in _appDBContext.KhachHangs on hd._MaKH equals kh.MaKh
                        select new HopDongViewModel
                        {
                            MaHopDong = hd.MaHopDong,
                            NgayBatDau = hd.NgayBatDau,
                            NgayKetThuc = hd.NgayKetThuc,
                            TienDatCoc = hd.TienDatCoc,
                            _MaPH = hd._MaPH,
                            _MaKH = hd._MaKH,
                            TenKhachHang = kh.TenKH,
                            TenPhong = p.Ten,
                            
                        };
            return query.ToList();
        }
    }
}
