using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.ViewModels;

namespace Motel.Controllers
{
    public class NhomNguoiDungController : Controller
    {
        private readonly INhomNguoiDungRepository Repository = null;

        public NhomNguoiDungController(INhomNguoiDungRepository repository)
        {
            this.Repository = repository;

        }
        public IActionResult Index()
        {
            QuanLyNhomNguoiDungViewModel model = new QuanLyNhomNguoiDungViewModel();
            model.listNhomNguoiDung = Repository.Gets();
            return View(model);
        }
    }
}