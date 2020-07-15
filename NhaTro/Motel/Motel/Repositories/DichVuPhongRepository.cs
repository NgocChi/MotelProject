using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class DichVuPhongRepository : IDichVuPhongRepository
    {
        private readonly AppDBContext _appDBContext;

        public DichVuPhongRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public async Task<int> Create(DichVuPhong dv)
        {
            if (dv != null)
            {
                _appDBContext.DichVuPhongs.Add(dv);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        public async Task<int> Update(DichVuPhong dv)
        {
            DichVuPhong find = await _appDBContext.DichVuPhongs.FindAsync(dv.MaDVPH);
            if (find != null)
            {
                find._MaDV = dv._MaDV;
                find._MaPH = dv._MaPH;
                _appDBContext.DichVuPhongs.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            DichVuPhong find = await _appDBContext.DichVuPhongs.FindAsync(id);
            if (find != null)
            {
                _appDBContext.DichVuPhongs.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
    }
}
