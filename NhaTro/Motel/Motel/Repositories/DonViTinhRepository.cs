using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class DonViTinhRepository : IDonViTinhRepository
    {
        private readonly AppDBContext _appDBContext;

        public DonViTinhRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public IEnumerable<DonViTinh> Gets()
        {
            return _appDBContext.DonViTinhs.ToList();
        }

        public async Task<DonViTinh> GetsById(int? id)
        {
            return await _appDBContext.DonViTinhs.FindAsync(id);
        }

        public async Task<int> Create(DonViTinh dvt)
        {
            if (dvt != null)
            {
                _appDBContext.DonViTinhs.Add(dvt);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        public async Task<int> Update(DonViTinh dvt)
        {
            DonViTinh find = await _appDBContext.DonViTinhs.FindAsync(dvt.MaDonVi);
            if (find != null)
            {
                find.TenDonVi = dvt.TenDonVi;
                find.MoTa = dvt.MoTa;
                _appDBContext.DonViTinhs.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            DonViTinh find = await _appDBContext.DonViTinhs.FindAsync(id);
            if (find != null)
            {
                _appDBContext.DonViTinhs.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
    }
}
