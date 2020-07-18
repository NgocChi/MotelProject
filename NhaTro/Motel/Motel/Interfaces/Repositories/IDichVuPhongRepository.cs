﻿using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IDichVuPhongRepository
    {
        int CheckExist(DichVuPhong dvp);
        Task<int> Create(DichVuPhong dvp);

        Task<int> Update(DichVuPhong dvp);

        Task<int> Delete(DichVuPhong id);
    }
}
