using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;

namespace Motel.Controllers
{
    public class NhaTroController : Controller
    {
        private readonly INhaTroRepository Repository = null;


        public NhaTroController(INhaTroRepository repository)
        {
            this.Repository = repository;
        }


        public ViewResult Index()
        {
            var listNt = Repository.Gets();
            return View(listNt);
        }
    }
}