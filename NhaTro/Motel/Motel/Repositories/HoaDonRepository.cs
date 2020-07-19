using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
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
                            _MaKhachHang = kh.MaKh,
                            TenPhong = p.Ten,
                            _MaPhong = p.MaPH,
                            _MaHopDong = hd.MaHopDong,
                            ThangNam = Thang,
                            TonTai = (from hoadon in _appDBContext.HoaDons where hoadon._MaHD == hd.MaHopDong select hoadon.MaHD).Contains(hd.MaHopDong),
                            TrangThai = false



                        };
            return query.ToList();
        }

        public async Task<int> Create(HoaDon hd)
        {
            if (hd != null)
            {
                _appDBContext.HoaDons.Add(hd);
                await _appDBContext.SaveChangesAsync();
                return hd.MaHD;
            }
            return 0;
        }
        public async Task<int> Update(HoaDon hd)
        {
            HoaDon find = _appDBContext.HoaDons.FirstOrDefault(p => p.MaHD == hd.MaHD);
            if (find != null)
            {

                _appDBContext.HoaDons.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> CreateCT(ChiTietHoaDon ct)
        {
            if (ct != null)
            {
                _appDBContext.ChiTietHoaDons.Add(ct);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        public async Task<int> UpdateCT(ChiTietHoaDon ct)
        {
            ChiTietHoaDon find = _appDBContext.ChiTietHoaDons.FirstOrDefault(p => p.MaCTHD == ct.MaCTHD);
            if (find != null)
            {

                _appDBContext.ChiTietHoaDons.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
    }
}
