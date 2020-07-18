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
        public async Task<int> Update(DichVuPhong dvp)
        {
            DichVuPhong find = _appDBContext.DichVuPhongs.Where(t => t._MaHD == dvp._MaHD && t._MaDV == dvp._MaDV && t._MaPH == t._MaPH).FirstOrDefault();
            if (find != null)
            {
                find._MaDV = dvp._MaDV;
                find._MaPH = dvp._MaPH;
                find._MaHD = dvp._MaHD;
                find.SoLuong = dvp.SoLuong;
                _appDBContext.DichVuPhongs.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> Delete(DichVuPhong dvp)
        {
            DichVuPhong find = _appDBContext.DichVuPhongs.Where(t => t._MaHD == dvp._MaHD && t._MaDV == dvp._MaDV && t._MaPH == t._MaPH).FirstOrDefault();
            if (find != null)
            {
                _appDBContext.DichVuPhongs.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public int CheckExist(DichVuPhong dvp)
        {
            DichVuPhong find = _appDBContext.DichVuPhongs.Where(t => t._MaHD == dvp._MaHD && t._MaDV == dvp._MaDV && t._MaPH == t._MaPH).FirstOrDefault();
            return find == null ? 1 : 0;
        }
    }
}
