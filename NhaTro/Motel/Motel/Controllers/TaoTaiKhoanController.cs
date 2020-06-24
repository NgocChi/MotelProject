using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;

namespace Motel.Controllers
{
    public class TaoTaiKhoanController : Controller
    {
        private readonly ITaiKhoanRepository Repository = null;

        public TaoTaiKhoanController(ITaiKhoanRepository repository)
        {
            Repository = repository;

        }
      
      
    }
}