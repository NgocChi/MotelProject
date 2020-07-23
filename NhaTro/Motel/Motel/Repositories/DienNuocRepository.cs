using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.Models.API.ElectrictyAndWaters;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class DienNuocRepository : IDienNuocRepository
    {
        private readonly AppDBContext _appDBContext;
        public DienNuocRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public DienNuocViewModel GetDienNuocByIdPhong(int idPhong, DateTime ngayghi)
        {
            var query = from dn in _appDBContext.DienNuocs
                        join p in _appDBContext.Phongs on dn.MaPH equals p.MaPH
                        where dn.MaPH == idPhong && dn.NgayGhiSo.Month == ngayghi.Month && dn.NgayGhiSo.Year == ngayghi.Year
                        select new DienNuocViewModel
                        {
                            NgayGhi = dn.NgayGhiSo,
                            CSCuDien = dn.CSDienCu == null ? 0 : dn.CSDienCu,
                            CSMoiDien = dn.CSDienMoi == null ? 0 : dn.CSDienMoi,
                            CSCuNuoc = dn.CSNuocCu == null ? 0 : dn.CSNuocCu,
                            CSMoiNuoc = dn.CSNuocMoi == null ? 0 : dn.CSNuocMoi,
                            MaPhong = dn.MaPH,
                            TieuThuDien = (dn.CSDienMoi - dn.CSDienCu) == null ? 0 : (dn.CSDienMoi - dn.CSDienCu),
                            TieuThuNuoc = (dn.CSNuocMoi - dn.CSNuocCu) == null ? 0 : (dn.CSNuocMoi - dn.CSNuocCu)

                        };
            return query.FirstOrDefault();
        }

        public async Task<int> Create(DienNuoc phong)
        {
            if (phong != null)
            {
                _appDBContext.DienNuocs.Add(phong);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;

        }
        public async Task<int> Update(DienNuoc phong)
        {

            DienNuoc find = _appDBContext.DienNuocs.FirstOrDefault(p => p.MaPH == phong.MaPH);
            if (find != null)
            {
                find.MaPH = phong.MaPH;
                find.NgayGhiSo = phong.NgayGhiSo;
                find.CSDienCu = phong.CSDienCu;
                find.CSDienMoi = phong.CSDienMoi;
                find.CSNuocCu = phong.CSNuocCu;
                find.CSNuocMoi = phong.CSNuocMoi;
                _appDBContext.DienNuocs.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;

        }

        public async Task<DienNuoc> GetById(int id)
        {
            return await _appDBContext.DienNuocs.FindAsync(id);
        }

        public IEnumerable<DienNuocViewModel> Gets()
        {
            var query = from dn in _appDBContext.DienNuocs
                        select new DienNuocViewModel
                        {
                            NgayGhi = dn.NgayGhiSo,
                            CSCuDien = dn.CSDienCu,
                            CSMoiDien = dn.CSDienMoi,
                            CSCuNuoc = dn.CSNuocCu,
                            CSMoiNuoc = dn.CSNuocMoi,
                            MaPhong = dn.MaPH,
                            TieuThuDien = (dn.CSDienMoi - dn.CSDienCu),
                            TieuThuNuoc = (dn.CSNuocMoi - dn.CSNuocCu)

                        };
            return query;
        }

        public async Task<ElectrictyAndWaterResponse> GetListElectrictyAndWaterByTime(ElectrictyAndWaterRequest request)
        {
           
            var dataFromDB = from dn in _appDBContext.DienNuocs
                             join ph in _appDBContext.Phongs on dn.MaPH equals ph.MaPH
                             join nt in _appDBContext.NhaTros on ph._MaNT equals nt.MaNT
                             where nt.MaNT == request.MaNhaTro &&
                             dn.NgayGhiSo.Year == request.YearTimeInput &&
                             dn.NgayGhiSo.Month == request.DayTimeInput
                             select new ElectrictyAndWaterDto
                             {
                                 ChiSoDienCu = dn.CSDienCu,
                                 ChiSoDienMoi = dn.CSDienMoi,
                                 ChiSoNuocCu = dn.CSNuocCu,
                                 ChiSoNuocMoi = dn.CSNuocMoi,
                                 MaPhong = ph.MaPH,
                                 TenPhong = ph.Ten,
                             };

            var response = new ElectrictyAndWaterResponse
            {
                Message = "OK",
                StatusCode = 0,
                DaChotSo = false,
                ElectrictyAndWaterDtos = dataFromDB.ToList(),
            };

            return response;
        }
    }
}
