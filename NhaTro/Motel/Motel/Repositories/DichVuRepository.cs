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


        public IEnumerable<DichVu_ViewModel> GetsByNhaTro(int id)
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
                            _MaDVi = ldv._MaDVi


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
    }
}
