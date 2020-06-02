using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Motel.Controllers
{
    public class DichVuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}