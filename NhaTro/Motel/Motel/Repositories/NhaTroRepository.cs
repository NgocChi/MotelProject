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
    public class NhaTroRepository : INhaTroRepository
    {
        private readonly AppDBContext _appDBContext;

        public NhaTroRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public IEnumerable<NhaTro> Gets()
        {
            return _appDBContext.NhaTros.ToList();
        }
        public IEnumerable<NhaTro> GetsList(string tentaikhoan)
        {
            var query = from nt in _appDBContext.NhaTros
                        join ct in _appDBContext.ChuTros on nt._MaChuTro equals ct.MaChuTro
                        join tk in _appDBContext.TaiKhoans on ct._TenTaiKhoan equals tk.TenTaiKhoan
                        where tk.TenTaiKhoan == tentaikhoan
                        select new NhaTro
                        {
                            MaNT = nt.MaNT,
                            Ten = nt.Ten,
                            DiaChi = nt.DiaChi,
                            Mota = nt.Mota,
                            TongPhong = _appDBContext.Phongs.Count(t => t._MaNT == nt.MaNT),
                            PhongTrong = _appDBContext.Phongs.Count(t => t._MaNT == nt.MaNT && (t._MaTTPH == 1 || t._MaTTPH == 2))


                        };
            return query.ToList();
        }

        public async Task<NhaTro> GetsById(int? id)
        {
            return await _appDBContext.NhaTros.FindAsync(id);
        }

        public int ThongPhong(int nhaTro)
        {
            int query = _appDBContext.Phongs.Where(t => t._MaNT == nhaTro).Count();
            return query;
        }

        public int ThongPhongTrong(int nhaTro)
        {
            int query = _appDBContext.Phongs.Where(t => t._MaNT == nhaTro && t._MaTTPH == 1).Count();
            return query;
        }

        public async Task<int> Create(NhaTro nhaTro)
        {
            if (nhaTro != null)
            {
                _appDBContext.NhaTros.Add(nhaTro);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        public async Task<int> Update(NhaTro nhaTro)
        {
            NhaTro find = await _appDBContext.NhaTros.FindAsync(nhaTro.MaNT);
            if (find != null)
            {
                find.Ten = nhaTro.Ten;
                find.TongPhong = nhaTro.TongPhong;
                find.PhongTrong = nhaTro.PhongTrong;
                find.Mota = nhaTro.Mota;
                find.DiaChi = nhaTro.DiaChi;
                _appDBContext.NhaTros.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            NhaTro find = await _appDBContext.NhaTros.FindAsync(id);
            if (find != null)
            {
                _appDBContext.NhaTros.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> UpdateSoLuongPhong(int maNt, int soLuong)
        {
            NhaTro find = _appDBContext.NhaTros.FirstOrDefault(p => p.MaNT == maNt);
            if (find != null)
            {
                find.TongPhong += soLuong;
                _appDBContext.NhaTros.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public int CheckForeignKey(int id)
        {
            Phong p = _appDBContext.Phongs.Where(t => t._MaNT == id).FirstOrDefault();
            return p == null ? 1 : 0;
        }
    }
}
