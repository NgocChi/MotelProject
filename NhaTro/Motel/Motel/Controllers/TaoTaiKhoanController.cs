using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;
using Web;

namespace Motel.Controllers
{
    public class TaoTaiKhoanController : Controller
    {
        private readonly ITaiKhoanRepository Repository = null;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;
        private string _taikhoan = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int _nhaTro = 0;

        public TaoTaiKhoanController(ITaiKhoanRepository repository, IPhanQuyenRepository phanQuyenRepository, IHttpContextAccessor httpContextAccessor)
        {
            Repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("MotelData");
            this.PhanQuyenRepository = phanQuyenRepository;

        }

        public IActionResult Index()
        {
            CommonViewModel model = new CommonViewModel();
            model.qlTaiKhoan.listTaiKhoan = Repository.Gets();
            model.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            return View(model);
        }


    }
}