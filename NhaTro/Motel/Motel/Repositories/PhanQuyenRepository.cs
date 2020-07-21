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
    public class PhanQuyenRepository : IPhanQuyenRepository
    {
        private readonly AppDBContext _appDBContext;

        public PhanQuyenRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public List<PhanQuyenViewModel> GetsManHinh(int idMaNND)
        {
            var query = from mh in _appDBContext.ManHinhs
                        join pq in _appDBContext.PhanQuyens.Where(t => t.MaNhomNguoiDung == idMaNND) on mh.MaManHinh equals pq.MaManHinh into result

                        from rs in result.DefaultIfEmpty()

                        select new PhanQuyenViewModel
                        {
                            TenManHinh = mh.TenManHinh,
                            KeyControl = mh.KeyControl,
                            MaManHinh = mh.MaManHinh,
                            CoQuyen = (rs.CoQuyen == null || rs.CoQuyen == false) ? false : true


                        };
            return query.ToList();
        }

        public int CheckForeignKey(int idMaNND, int idMaMH)
        {
            PhanQuyen pquyen = _appDBContext.PhanQuyens.Where(t => t.MaManHinh == idMaMH && t.MaNhomNguoiDung == idMaNND).FirstOrDefault();
            return pquyen == null ? 1 : 0;
        }

        public async Task<int> Create(PhanQuyen pquyen)
        {
            if (pquyen != null)
            {
                _appDBContext.PhanQuyens.Add(pquyen);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;

        }
        public async Task<int> Update(PhanQuyen pquyen)
        {

            PhanQuyen find = _appDBContext.PhanQuyens.FirstOrDefault(p => p.MaManHinh == pquyen.MaManHinh && p.MaNhomNguoiDung == pquyen.MaNhomNguoiDung);
            if (find != null)
            {

                find.CoQuyen = pquyen.CoQuyen;
                _appDBContext.PhanQuyens.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;

        }
    }
}
