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
    public class NhaTroRepository: INhaTroRepository
    {
        private readonly AppDBContext _appDBContext;

        public NhaTroRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public  IEnumerable<NhaTro> Gets()
        {
            return _appDBContext.NhaTros.ToList();
        }

        public async Task<NhaTro> GetsById(int? id)
        {
            return await  _appDBContext.NhaTros.FindAsync( id);
        }

        public async Task<int> Create(NhaTro nhaTro)
        {
            if (nhaTro != null)
            {
                _appDBContext.NhaTros.Add(nhaTro);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;

        }
        public async Task<int> Update(NhaTro nhaTro)
        {

            NhaTro find = await _appDBContext.NhaTros.FindAsync(nhaTro.MaNT);
            if(find != null)
            {
                find.Ten = nhaTro.Ten;
                find.TongPhong = nhaTro.TongPhong;
                find.PhongTrong = nhaTro.PhongTrong;
                find.Mota = nhaTro.Mota;
                find.DiaChi = nhaTro.DiaChi;
                _appDBContext.NhaTros.Add(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public int UpdateSoLuongPhong(int maNt , int soLuong)
        {

            NhaTro find = _appDBContext.NhaTros.FirstOrDefault(p => p.MaNT == maNt);
            if (find != null)
            {
                find.TongPhong = soLuong;
                _appDBContext.NhaTros.Add(find);
                _appDBContext.SaveChanges();
                return 1;
            }
            return 0;

        }
    }
}
