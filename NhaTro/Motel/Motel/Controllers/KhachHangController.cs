using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Motel.Queries;
using Motel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controls;

namespace Motel.Controllers
{
   
    public class KhachHangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }





    }
}
