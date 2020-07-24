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
        public IEnumerable<PhongViewModel> Gets(int _nhaTro)
        {
            var query = from p in _appDBContext.Phongs
                        join nt in _appDBContext.NhaTros on p._MaNT equals nt.MaNT
                        join ttp in _appDBContext.TrangThaiPhongs on p._MaTTPH equals ttp.MaTTPH
                        join lp in _appDBContext.LoaiPhongs on p._MaLP equals lp.MaLP
                        where p._MaNT == _nhaTro
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
        public IEnumerable<Phong> GetsPhongTrong(int idPhong)
        {
            var query = _appDBContext.Phongs.Where(t => t._MaTTPH == 1 || t.MaPH == idPhong);
            return query.ToList();
        }
        public IEnumerable<PhongViewModel> GetsPTrong(int idNT, int idPhong)
        {
            var query = from p in _appDBContext.Phongs
                        join nt in _appDBContext.NhaTros on p._MaNT equals nt.MaNT
                        join ttp in _appDBContext.TrangThaiPhongs on p._MaTTPH equals ttp.MaTTPH
                        join lp in _appDBContext.LoaiPhongs on p._MaLP equals lp.MaLP
                        where (p._MaTTPH == 1 && p._MaNT == idNT) || p.MaPH == idPhong
                        select new PhongViewModel
                        {
                            MaPH = p.MaPH,
                            Tang = p.Tang,
                            Ten = p.Ten + '-' + lp.Gia + '-' + lp.GiaDatCoc,
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
        public async Task<Phong> GetById(int id)
        {
            return await _appDBContext.Phongs.FindAsync(id);
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
        public async Task<int> CreateList(PhongViewModel phong)
        {
            if (phong != null)
            {
                for (int i = 1; i <= phong.SoPhongTrenTang; i++)
                {
                    Phong p = new Phong();
                    p.Tang = phong.Tang;
                    p._MaNT = phong._MaNT;
                    p.SoNguoiToiDa = phong.SoNguoiToiDa;
                    p._MaTTPH = 1;
                    p._MaLP = phong._MaLP;
                    p.CSDien = 0;
                    p.CSNuoc = 0;
                    p.Ten = "Phòng " + phong.Tang + "0" + i;
                    _appDBContext.Phongs.Add(p);

                }
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

        public int CheckForeignKey(int id)
        {
            DatPhong p = _appDBContext.DatPhongs.Where(t => t._MaPH == id).FirstOrDefault();
            HopDong hd = _appDBContext.HopDongs.Where(t => t._MaPH == id).FirstOrDefault();
            return p == null && hd == null ? 1 : 0;
        }

        public IEnumerable<(int MaPhong, string TenPhong, int MaNhaTro, string TenNhaTro)> GetPhongDangHoatDongByKhachThue(int maKhachThue)
        {
            var listData = (from phong in _appDBContext.Phongs
                       join nhatro in _appDBContext.NhaTros on phong._MaNT equals nhatro.MaNT
                       join hd in _appDBContext.HopDongs on phong.MaPH equals hd._MaPH
                       where phong._MaTTPH == 3 && hd._MaKH == maKhachThue //phong dang hoat dong
                       select new { MaPhong = phong.MaPH, TenPhong = phong.Ten, MaNhaTro = nhatro.MaNT, TenNhaTro = nhatro.Ten }).ToList();
           
            List<(int MaPhong, string TenPhong, int MaNhaTro, string TenNhaTro)> result = new List<(int MaPhong, string TenPhong, int MaNhaTro, string TenNhaTro)>();
            foreach (var item in listData)
            {
                result.Add((item.MaPhong, item.TenPhong, item.MaNhaTro, item.TenNhaTro));
            }
            return result;
        }
        public PhongViewModel GetByIdPhong(int idPhong)
        {
            var query = from p in _appDBContext.Phongs
                        join lp in _appDBContext.LoaiPhongs on p._MaLP equals lp.MaLP
                        where p.MaPH == idPhong
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
                            TenLoaiPhong = lp.Ten,
                            Gia = lp.Gia,
                            GiaDatCoc = lp.GiaDatCoc,
                            DienTich = lp.DienTich

                        };
            return query.FirstOrDefault();
        }

    }
}
