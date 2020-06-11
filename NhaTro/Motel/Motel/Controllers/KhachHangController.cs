using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Motel.Interfaces.Repositories;
using Motel.Models;
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
        private readonly IKhachHangRepository Queries = null;
       

        public KhachHangController(IKhachHangRepository queries)
        {
            this.Queries = queries;
        }

        public  ViewResult Index()
        {
            var listKh =  Queries.Gets();
            return View(listKh);
        }





    }
}
