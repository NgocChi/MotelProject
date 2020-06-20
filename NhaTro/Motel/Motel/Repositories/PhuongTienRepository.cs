using Motel.Data;
using Motel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class PhuongTienRepository : IPhuongTienRepository
    {
        private readonly AppDBContext _appDBContext;

        public PhuongTienRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
    }
}
