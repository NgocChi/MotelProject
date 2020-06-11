using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
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

        public IEnumerable<NhaTro> Gets()
        {
            return _appDBContext.NhaTros.ToList();
        }
    }
}
