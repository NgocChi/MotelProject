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
    public class PhongRepository : IPhongRepository
    {
        private readonly AppDBContext _appDBContext;

        public PhongRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public PhongViewModel Gets()
        {
            PhongViewModel phong = new PhongViewModel();
            phong.listPhong = _appDBContext.Phongs.ToList();
            return phong;
        }
    }
}
