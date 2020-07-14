using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class ChuTroRepository : IChuTroRepository
    {
        private readonly AppDBContext _appDBContext;


        public ChuTroRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public IEnumerable<ChuTro> Gets()
        {
            return _appDBContext.ChuTros.ToList();
        }

    }
}
