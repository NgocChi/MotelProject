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
    public class DatPhongRepository : IDatPhongRepository
    {
        private readonly AppDBContext _appDBContext;


        public DatPhongRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }


        public IEnumerable<DatPhongViewModel> Gets()
        {
            var query = from dp in _appDBContext.DatPhongs
                        join p in _appDBContext.Phongs on dp._MaPH equals p.MaPH
                        join kh in _appDBContext.KhachHangs on dp._MaKH equals kh.MaKh
                        select new DatPhongViewModel
                        {
                            MaDP = dp.MaDP,
                            NgayDat = dp.NgayDat,
                            NgayHetHan = dp.NgayHetHan,
                            SoTienCoc = dp.SoTienCoc,
                            _MaPH = dp._MaPH,
                            _MaKH = dp._MaKH,
                            TenKhachHang = kh.TenKH,
                            TenPhong = p.Ten,
                            SoDienThoai = kh.SoDienThoai
                        };
            return query.ToList();
        }
        public IEnumerable<DatPhongViewModel> GetsByMaNhaTro(int id)
        {
            var query = from dp in _appDBContext.DatPhongs
                        join p in _appDBContext.Phongs on dp._MaPH equals p.MaPH
                        join kh in _appDBContext.KhachHangs on dp._MaKH equals kh.MaKh
                        where p._MaNT == id
                        select new DatPhongViewModel
                        {
                            MaDP = dp.MaDP,
                            NgayDat = dp.NgayDat,
                            NgayHetHan = dp.NgayHetHan,
                            SoTienCoc = dp.SoTienCoc,
                            _MaPH = dp._MaPH,
                            _MaKH = dp._MaKH,
                            TenKhachHang = kh.TenKH,
                            TenPhong = p.Ten,
                            SoDienThoai = kh.SoDienThoai
                        };
            return query.ToList();
        }

        public async Task<int> Create(DatPhong datphong)
        {
            if (datphong != null)
            {
                _appDBContext.DatPhongs.Add(datphong);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        public async Task<int> Update(DatPhong datphong)
        {
            DatPhong find = _appDBContext.DatPhongs.FirstOrDefault(p => p.MaDP == datphong.MaDP);
            Phong ph = _appDBContext.Phongs.Find(find._MaPH);
            ph._MaTTPH = 1;
            _appDBContext.Phongs.Update(ph);

            if (find != null)
            {
                find.NgayDat = datphong.NgayDat;
                find.NgayHetHan = datphong.NgayHetHan;
                find.SoTienCoc = datphong.SoTienCoc;
                find._MaKH = datphong._MaKH;
                find._MaPH = datphong._MaPH;
                find.GhiChu = datphong.GhiChu;
                _appDBContext.DatPhongs.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            DatPhong find = await _appDBContext.DatPhongs.FindAsync(id);
            if (find != null)
            {
                _appDBContext.DatPhongs.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<DatPhong> GetsById(int? id)
        {
            return await _appDBContext.DatPhongs.FindAsync(id);
        }
    }
}
