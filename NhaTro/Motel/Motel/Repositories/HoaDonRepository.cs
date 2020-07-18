using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private readonly AppDBContext _appDBContext;

        public HoaDonRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public IEnumerable<HoaDonViewModel> Gets(int id, DateTime Thang)
        {
            var query = from hd in _appDBContext.HopDongs
                        join hdon in _appDBContext.HoaDons on hd.MaHopDong equals hdon._MaHD into result
                        from rs in result.DefaultIfEmpty()
                        join p in _appDBContext.Phongs on hd._MaPH equals p.MaPH
                        join kh in _appDBContext.KhachHangs on hd._MaKH equals kh.MaKh

                        where p._MaNT == id
                        select new HoaDonViewModel
                        {
                            TenKhachHang = kh.TenKH,
                            TenPhong = p.Ten,
                            ThangNam = Thang,
                            TonTai = (from hoadon in _appDBContext.HoaDons where hoadon._MaHD == hd.MaHopDong select hoadon.MaHD).Contains(hd.MaHopDong),
                            TrangThai = false



                        };
            return query.ToList();
        }
    }
}
