using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.Queries;
using Motel.Repositories;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controls;

namespace Motel.Controllers
{
   
    public class KhachHangController : Controller
    {
        private readonly IKhachHangRepository Repository = null;
        public KhachHang khach;
       

        public KhachHangController(IKhachHangRepository queries)
        {
            this.Repository = queries;
        }

        public ViewResult Index()
        {
            KhachHangViewModel kh = new KhachHangViewModel();
            kh.list = Repository.Gets();
            return View(kh);
        }

       
        [HttpPost]
        public IActionResult Save(KhachHang kh)
        {
           
            return RedirectToAction("Suscess");
        }





    }
}
