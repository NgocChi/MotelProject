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
    public class LoaiDichVuRepository : ILoaiDichVuRepository
    {
        private readonly AppDBContext _appDBContext;

        public LoaiDichVuRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public IEnumerable<LoaiDichVuView> Gets()
        {
            var query = from loai in _appDBContext.LoaiDichVus
                        join dv in _appDBContext.DonViTinhs on loai._MaDVi equals dv.MaDonVi

                        select new LoaiDichVuView
                        {
                            MaLoaiDV = loai.MaLoaiDV,
                            TenLoaiDV = loai.TenLoaiDV,
                            DonGia = loai.DonGia,
                            Mota = loai.Mota,
                            TenDonViTinh = dv.TenDonVi,
                            _MaDVi = loai._MaDVi

                        };
            return query.ToList();
        }

        public List<LoaiDichVuViewModel1> GetList()
        {
            var query = from loai in _appDBContext.LoaiDichVus
                        join dv in _appDBContext.DonViTinhs on loai._MaDVi equals dv.MaDonVi

                        select new LoaiDichVuViewModel1
                        {
                            MaLoaiDV = loai.MaLoaiDV,
                            TenLoaiDV = loai.TenLoaiDV,
                            IsCheck = false

                        };
            return query.ToList();
        }

        public async Task<LoaiDichVu> GetsById(int? id)
        {
            return await _appDBContext.LoaiDichVus.FindAsync(id);
        }

        public async Task<int> Create(LoaiDichVu dvt)
        {
            if (dvt != null)
            {
                _appDBContext.LoaiDichVus.Add(dvt);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        public async Task<int> Update(LoaiDichVu dvt)
        {
            LoaiDichVu find = await _appDBContext.LoaiDichVus.FindAsync(dvt.MaLoaiDV);
            if (find != null)
            {
                find.TenLoaiDV = dvt.TenLoaiDV;
                find.DonGia = dvt.DonGia;
                find.Mota = dvt.Mota;
                find._MaDVi = dvt._MaDVi;
                _appDBContext.LoaiDichVus.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            LoaiDichVu find = await _appDBContext.LoaiDichVus.FindAsync(id);
            if (find != null)
            {
                _appDBContext.LoaiDichVus.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
    }
}
