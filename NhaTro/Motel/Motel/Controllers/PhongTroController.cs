using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;

namespace Motel.Controllers
{
    public class PhongTroController : Controller
    {
        private readonly IPhongRepository Repository = null;


        public PhongTroController(IPhongRepository repository)
        {
            this.Repository = repository;
        }


        public ViewResult Index()
        {
            var listPt = Repository.Gets();
            return View(listPt);
        }
       
    }
}