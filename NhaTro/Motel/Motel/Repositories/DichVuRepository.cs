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
    public class DichVuRepository : IDichVuRepository
    {
        private readonly AppDBContext _appDBContext;

        public DichVuRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public IEnumerable<DichVu> Gets()
        {
            return _appDBContext.DichVus.ToList();
        }


        public List<DichVu_ViewModel> GetsByNhaTro(int id, int idPhong)
        {
            var query = from dv in _appDBContext.DichVus
                        join nt in _appDBContext.NhaTros on dv._MaNT equals nt.MaNT
                        join dvt in _appDBContext.DonViTinhs on dv._MaDVT equals dvt.MaDonVi
                        where dv._MaNT == id

                        select new DichVu_ViewModel
                        {
                            Ten = dv.Ten,
                            Gia = dv.Gia,
                            MoTa = dv.MoTa,
                            MaDV = dv.MaDV,
                            _MaNT = dv._MaNT,
                            _MaDVT = dv._MaDVT,
                            TenDonVi = dvt.TenDonVi,
                            TenNhaTro = nt.Ten,
                            SoLuong = 0,
                            IsCheck = (from dvp in _appDBContext.DichVuPhongs where dvp._MaPH == idPhong select dvp._MaDV).Contains(dv.MaDV)



                        };
            return query.ToList();
        }

        public List<DichVu_ViewModel> GetsByNhaTroDEMO(int id, int idHopDong)
        {
            var query = from dv in _appDBContext.DichVus
                        join nt in _appDBContext.NhaTros on dv._MaNT equals nt.MaNT
                        join dvt in _appDBContext.DonViTinhs on dv._MaDVT equals dvt.MaDonVi
                        join dvp in _appDBContext.DichVuPhongs.Where(t => t._MaHD == idHopDong) on dv.MaDV equals dvp._MaDV into result
                        from rs in result.DefaultIfEmpty()
                        where dv._MaNT == id

                        select new DichVu_ViewModel
                        {
                            Ten = dv.Ten,
                            Gia = dv.Gia,
                            MoTa = dv.MoTa,
                            MaDV = dv.MaDV,
                            _MaNT = dv._MaNT,
                            _MaDVT = dv._MaDVT,
                            TenDonVi = dvt.TenDonVi,
                            TenNhaTro = nt.Ten,
                            SoLuong = rs.SoLuong,
                            IsCheck = (from dvp in _appDBContext.DichVuPhongs where dvp._MaHD == idHopDong select dvp._MaDV).Contains(dv.MaDV)
                        };
            return query.ToList();
        }

        public List<DichVu_ViewModel> GetsByIdPhongIdHopDong(int idHopDong, int idPhong)
        {
            var query = from dvp in _appDBContext.DichVuPhongs
                        join hd in _appDBContext.HopDongs on dvp._MaHD equals hd.MaHopDong
                        join p in _appDBContext.Phongs on dvp._MaPH equals p.MaPH
                        join dv in _appDBContext.DichVus on dvp._MaDV equals dv.MaDV
                        join dvt in _appDBContext.DonViTinhs on dv._MaDVT equals dvt.MaDonVi
                        where dvp._MaHD == idHopDong && dvp._MaPH == idPhong
                        select new DichVu_ViewModel
                        {
                            Gia = dv.Gia,
                            Ten = dv.Ten,
                            TenDonVi = dvt.TenDonVi,
                            SoLuong = dvp.SoLuong,
                            _MaDichVuPhong = dvp.MaDVPH,
                            MacDinh = dv.MacDinh == null ? false : true
                        };
            return query.ToList();
        }

        public List<DichVu_ViewModel> GetsByNhaTro(int id)
        {
            var query = from dv in _appDBContext.DichVus
                        join nt in _appDBContext.NhaTros on dv._MaNT equals nt.MaNT
                        join dvt in _appDBContext.DonViTinhs on dv._MaDVT equals dvt.MaDonVi
                        where dv._MaNT == id

                        select new DichVu_ViewModel
                        {
                            Ten = dv.Ten,
                            Gia = dv.Gia,
                            MoTa = dv.MoTa,
                            MaDV = dv.MaDV,
                            _MaNT = dv._MaNT,
                            _MaDVT = dv._MaDVT,
                            TenDonVi = dvt.TenDonVi,
                            TenNhaTro = nt.Ten



                        };
            return query.ToList();
        }



        public async Task<DichVu> GetsById(int? id)
        {
            return await _appDBContext.DichVus.FindAsync(id);
        }

        public LoaiDichVuView GetsByIdMaLoaiDV(int? id)
        {
            var query = from ldv in _appDBContext.LoaiDichVus
                        where ldv.MaLoaiDV == id
                        select new LoaiDichVuView
                        {
                            MaLoaiDV = ldv.MaLoaiDV,
                            TenLoaiDV = ldv.TenLoaiDV,
                            DonGia = ldv.DonGia,
                            Mota = ldv.Mota,
                            _MaDVi = ldv._MaDVi,
                            MacDinh = ldv.MacDinh


                        };
            return query.FirstOrDefault();
        }

        public async Task<int> Create(DichVu dv)
        {
            if (dv != null)
            {
                _appDBContext.DichVus.Add(dv);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        public async Task<int> Update(DichVu dv)
        {
            DichVu find = await _appDBContext.DichVus.FindAsync(dv.MaDV);
            if (find != null)
            {
                find.Gia = dv.Gia;
                find.Ten = dv.Ten;
                find.MoTa = dv.MoTa;
                find._MaDVT = dv._MaDVT;
                find.MacDinh = dv.MacDinh;
                _appDBContext.DichVus.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            DichVu find = await _appDBContext.DichVus.FindAsync(id);
            if (find != null)
            {
                _appDBContext.DichVus.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public int CheckForeignKey(int id)
        {
            DichVuPhong p = _appDBContext.DichVuPhongs.Where(t => t._MaDV == id).FirstOrDefault();
            return p == null ? 1 : 0;
        }
    }
}
