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
    public class HopDongRepository : IHopDongRepository
    {
        private readonly AppDBContext _appDBContext;

        public HopDongRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public IEnumerable<HopDongViewModel> Gets(int idNhaTro)
        {
            var query = from hd in _appDBContext.HopDongs
                        join p in _appDBContext.Phongs on hd._MaPH equals p.MaPH
                        join kh in _appDBContext.KhachHangs on hd._MaKH equals kh.MaKh
                        where p._MaNT == idNhaTro
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

        public async Task<int> Create(HopDong hopDong)
        {
            if (hopDong != null)
            {
                _appDBContext.HopDongs.Add(hopDong);
                await _appDBContext.SaveChangesAsync();
                return hopDong.MaHopDong;
            }
            return 0;
        }
        public async Task<int> Update(HopDong hopDong)
        {
            HopDong find = _appDBContext.HopDongs.FirstOrDefault(p => p.MaHopDong == hopDong.MaHopDong);
            if (find != null)
            {
                find.NgayBatDau = hopDong.NgayBatDau;
                find.NgayKetThuc = hopDong.NgayKetThuc;
                find._MaKH = hopDong._MaKH;
                find._MaPH = hopDong._MaPH;
                find.GiaPhong = hopDong.GiaPhong;
                find.TienDatCoc = hopDong.TienDatCoc;
                find._MaCT = hopDong._MaCT;
                find.GhiChu = hopDong.GhiChu;
                find.SoDien = hopDong.SoDien;
                find.SoNuoc = hopDong.SoNuoc;
                _appDBContext.HopDongs.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            HopDong find = await _appDBContext.HopDongs.FindAsync(id);
            if (find != null)
            {
                _appDBContext.HopDongs.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<HopDong> GetById(int id)
        {
            return await _appDBContext.HopDongs.FindAsync(id);
        }
    }
}
