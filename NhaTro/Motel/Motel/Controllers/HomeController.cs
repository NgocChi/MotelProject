using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Controls;
using Microsoft.Extensions.Configuration;
using Motel.Queries;
using Motel.Repositories;
using Motel.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Web;
using Motel.Models;
using System.Collections.Generic;

namespace Motel.Controllers
{

    public class HomeController : Controller
    {
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;
        private string _taikhoan = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IPhanQuyenRepository phanQuyenRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.PhanQuyenRepository = phanQuyenRepository;
            _httpContextAccessor = httpContextAccessor;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");
        }

        public IActionResult Index()
        {
            List<ManHinh> list = new List<ManHinh>();
            list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            return View(list);

        }

    }
}