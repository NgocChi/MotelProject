﻿using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IPhongRepository
    {
        IEnumerable<Phong> Gets();
    }
}
