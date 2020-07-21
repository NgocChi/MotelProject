using Motel.Data;
using Motel.Interfaces.Repositories;
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

        public List<PhanQuyenViewModel> GetsManHinh()
        {
            var query = from mh in _appDBContext.ManHinhs
                        join pq in _appDBContext.PhanQuyens on mh.MaManHinh equals pq.MaManHinh
                        select new PhanQuyenViewModel
                        {
                            TenManHinh = mh.TenManHinh,
                            KeyControl = mh.KeyControl,
                            MaManHinh = mh.MaManHinh,
                            CoQuyen = pq.CoQuyen


                        };
            return query.ToList();
        }
    }
}
