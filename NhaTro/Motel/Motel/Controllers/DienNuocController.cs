﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Motel.Controllers
{
    public class DienNuocController : Controller
    {

        public DienNuocController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}