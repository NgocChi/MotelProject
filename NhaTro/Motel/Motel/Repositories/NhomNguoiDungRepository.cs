using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class NhomNguoiDungRepository : INhomNguoiDungRepository
    {
        private readonly AppDBContext _appDBContext;
        public NhomNguoiDungRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public IEnumerable<NhomNguoiDung> Gets()
        {
            return _appDBContext.NhomNguoiDungs.ToList();
        }
    }
}
