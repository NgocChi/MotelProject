using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
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
    }
}
