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

        public NhaTroViewModel Gets()
        {
            NhaTroViewModel list = new NhaTroViewModel();
            list.listNhaTro = _appDBContext.NhaTros.ToList();
            return list;
        }
    }
}
