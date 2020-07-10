using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class LoaiPhongRepository : ILoaiPhongRepository
    {
        private readonly AppDBContext _appDBContext;
        public LoaiPhongRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public async Task<int> CreateLoaiPhong(LoaiPhong loaiPhong)
        {
            if (loaiPhong != null)
            {
                _appDBContext.LoaiPhongs.Add(loaiPhong);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        public async Task<int> UpdateLoaiPhong(LoaiPhong loaiPhong)
        {
            LoaiPhong find = _appDBContext.LoaiPhongs.FirstOrDefault(p => p.MaLP == loaiPhong.MaLP);
            if (find != null)
            {
                find.Ten = loaiPhong.Ten;
                find.Gia = loaiPhong.Gia;
                find.GiaDatCoc = loaiPhong.GiaDatCoc;
                find.DienTich = loaiPhong.DienTich;
                find.ThongTin = loaiPhong.ThongTin;
                _appDBContext.LoaiPhongs.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> DeleteLoaiPh(int id)
        {
            LoaiPhong find = await _appDBContext.LoaiPhongs.FindAsync(id);
            if (find != null)
            {
                _appDBContext.LoaiPhongs.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<LoaiPhong> GetLoaiPhById(int id)
        {
            return await _appDBContext.LoaiPhongs.FindAsync(id);
        }

        public IEnumerable<LoaiPhong> GetsLoaiPhong()
        {
            var query = _appDBContext.LoaiPhongs.ToList();
            return query;
        }
    }
}
