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
        private readonly IKhachHangRepository Repository = null;
        public KhachHang khach;
       

        public KhachHangController(IKhachHangRepository queries)
        {
            this.Repository = queries;
        }

        //public  ViewResult Index()
        //{
        //    var listKh = Repository.Gets();
        //    return View(listKh);
        //}

        public ViewResult Save()
        {
            var listKh = Repository.Gets();
            return View(listKh);
        }
        [HttpPost]
        public IActionResult Save(KhachHang kh)
        {
            //if (ModelState.IsValid)
            //{
            //    Repository.Save(kh);
            //    return RedirectToAction("Suscess");
            //}

            //return View(kh);
            //Repository.Save(kh);
            return RedirectToAction("Suscess");
        }





    }
}
