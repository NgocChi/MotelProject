using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class PhongRepository : IPhongRepository
    {
        private readonly AppDBContext _appDBContext;

        public PhongRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public IEnumerable<Phong> Gets()
        {
            return _appDBContext.Phongs.ToList();
        }
    }
}
