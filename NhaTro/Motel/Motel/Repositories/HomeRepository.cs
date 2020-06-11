using Motel.Data;
using Motel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly AppDBContext _appDBContext;

        public HomeRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
    }
}
