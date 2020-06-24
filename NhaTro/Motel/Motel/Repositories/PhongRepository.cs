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
    public class PhongRepository : IPhongRepository
    {
        private readonly AppDBContext _appDBContext;
        public PhongRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public IEnumerable<PhongViewModel> Gets()
        {
            var query = from p in _appDBContext.Phongs
                        join nt in _appDBContext.NhaTros on p._MaNT equals nt.MaNT
                        join ttp in _appDBContext.TrangThaiPhongs on p._MaTTPH equals ttp.MaTTPH
                        join lp in _appDBContext.LoaiPhongs on p._MaLP equals lp.MaLP
                        select new PhongViewModel
                        {
                            MaPH = p.MaPH,
                            Tang = p.Tang,
                            Ten = p.Ten,
                            CSDien = p.CSDien,
                            CSNuoc = p.CSNuoc,
                            _MaLP = p._MaLP,
                            _MaTTPH = p._MaTTPH,
                            SoNguoiToiDa = p.SoNguoiToiDa,
                            TenNhaTro = nt.Ten,
                            TrangThai = ttp.Ten,
                            TenLoaiPhong = lp.Ten,
                            Gia = lp.Gia,
                            GiaDatCoc = lp.GiaDatCoc,
                            DienTich = lp.DienTich

                        };
            return query.ToList();
        }
        public IEnumerable<Phong> GetsPhongTrong()
        {
            var query = _appDBContext.Phongs.Where(t => t._MaTTPH == 1);
            return query.ToList();
        }
        public async Task<Phong> GetById(int id)
        {
            return await _appDBContext.Phongs.FindAsync(id);
        }
        public async Task<LoaiPhong> GetLoaiPhById(int id)
        {
            return await _appDBContext.LoaiPhongs.FindAsync(id);
        }
        public IEnumerable<LoaiPhong> GetsLoaiPhong()
        {
            var query = _appDBContext.LoaiPhongs.ToList();
            return query;
        }

        public IEnumerable<TrangThaiPhong> GetsTrangThaiPhong()
        {
            var query = _appDBContext.TrangThaiPhongs.ToList();
            return query;
        }

        public async Task<int> Create(Phong phong)
        {
            if (phong != null)
            {
                _appDBContext.Phongs.Add(phong);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;

        }
        public async Task<int> Update(Phong phong)
        {

            Phong find = _appDBContext.Phongs.FirstOrDefault(p => p.MaPH == phong.MaPH);
            if (find != null)
            {
                find.Ten = phong.Ten;
                find.CSDien = phong.CSDien;
                find.CSNuoc = phong.CSNuoc;
                find._MaLP = phong._MaLP;
                find._MaTTPH = phong._MaTTPH;
                find.SoNguoiToiDa = phong.SoNguoiToiDa;
                _appDBContext.Phongs.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;

        }
        public async Task<int> UpdateTTP(int maph, int ttph)
        {
            Phong find = await _appDBContext.Phongs.FindAsync(maph);
            if (find != null)
            {
                find._MaTTPH = ttph;
                _appDBContext.Phongs.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> CreateLoaiPhong(LoaiPhong loaiPhong)
        {
            if (loaiPhong != null)
            {
                _appDBContext.LoaiPhongs.Add(loaiPhong);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        public async Task<int> UpdateLoaiPhong(LoaiPhong loaiPhong)
        {
            LoaiPhong find = _appDBContext.LoaiPhongs.FirstOrDefault(p => p.MaLP == loaiPhong.MaLP);
            if (find != null)
            {
                find.Ten = loaiPhong.Ten;
                find.Gia = loaiPhong.Gia;
                find.GiaDatCoc = loaiPhong.GiaDatCoc;
                find.DienTich = loaiPhong.DienTich;
                find.ThongTin = loaiPhong.ThongTin;
                _appDBContext.LoaiPhongs.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            Phong find = await _appDBContext.Phongs.FindAsync(id);
            if (find != null)
            {
                _appDBContext.Phongs.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> DeleteLoaiPh(int id)
        {
            LoaiPhong find = await _appDBContext.LoaiPhongs.FindAsync(id);
            if (find != null)
            {
                _appDBContext.LoaiPhongs.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
    }
}
