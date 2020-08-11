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
                        join hopdong in _appDBContext.HopDongs on dn.MaPH equals hopdong._MaPH
                        where dn.MaPH == idPhong && dn.NgayGhiSo.Month == ngayghi.Month && dn.NgayGhiSo.Year == ngayghi.Year
                        select new DienNuocViewModel
                        {
                            MaDienNuoc = dn.MaDienNuoc,
                            NgayGhi = dn.NgayGhiSo,
                            CSCuDien = hopdong.SoDien,
                            CSMoiDien = dn.CSDienMoi == null ? 0 : dn.CSDienMoi,
                            CSCuNuoc = hopdong.SoNuoc,
                            CSMoiNuoc = dn.CSNuocMoi == null ? 0 : dn.CSNuocMoi,
                            MaPhong = dn.MaPH,
                            TieuThuDien = (dn.CSDienMoi - hopdong.SoDien) == null ? 0 : (dn.CSDienMoi - hopdong.SoDien),
                            TieuThuNuoc = (dn.CSNuocMoi - hopdong.SoNuoc) == null ? 0 : (dn.CSNuocMoi - hopdong.SoNuoc)

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

            DienNuoc find = _appDBContext.DienNuocs.FirstOrDefault(p => p.MaDienNuoc == phong.MaDienNuoc);
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
        public async Task<int> UpdateChotSo(int idDN)
        {

            DienNuoc find = _appDBContext.DienNuocs.Find(idDN);
            if (find != null)
            {
                find.DaChotSo = true;
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

        public IEnumerable<DienNuocViewModel> Gets(DateTime ThangNam, int nhatro)
        {
            var query = from dn in _appDBContext.DienNuocs
                        join phong in _appDBContext.Phongs on dn.MaPH equals phong.MaPH
                        where dn.NgayGhiSo.Year == ThangNam.Year && dn.NgayGhiSo.Month == ThangNam.Month && phong._MaNT == nhatro
                        select new DienNuocViewModel
                        {
                            DaChotSo = dn.DaChotSo,
                            MaDienNuoc = dn.MaDienNuoc,
                            NgayGhi = dn.NgayGhiSo,
                            CSCuDien = dn.CSDienCu,
                            CSMoiDien = dn.CSDienMoi,
                            CSCuNuoc = dn.CSNuocCu,
                            CSMoiNuoc = dn.CSNuocMoi,
                            MaPhong = dn.MaPH,
                            TenPhong = phong.Ten,
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
