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

        public int Create(DatPhong datphong)
        {
            if (datphong != null)
            {
                _appDBContext.DatPhongs.Add(datphong);
                
                _appDBContext.SaveChanges();
                return 1;
            }
            return 0;

        }
        public int Update(DatPhong datphong)
        {

            //Phong find = _appDBContext.DatPhongs.FirstOrDefault(p => p.MaPH == datphong.MaPH);
            //if (find != null)
            //{
            //    find.Ten = datphong.Ten;
            //    find.CSDien = datphong.CSDien;
            //    find.CSNuoc = datphong.CSNuoc;
            //    find._MaLP = datphong._MaLP;

            //    _appDBContext.DatPhongs.Add(find);
            //    _appDBContext.SaveChanges();
            //    return 1;
            //}
            return 0;

        }
    }
}
