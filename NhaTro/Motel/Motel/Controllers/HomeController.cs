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
using Motel.ViewModels;

namespace Motel.Controllers
{

    public class HomeController : Controller
    {
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;
        private readonly INhaTroRepository NhaTroRepository = null;
        private string _taikhoan = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int _nhaTro = 0;
        public HomeController(INhaTroRepository nhaTroRepository, IPhanQuyenRepository phanQuyenRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.PhanQuyenRepository = phanQuyenRepository;
            _httpContextAccessor = httpContextAccessor;
            this.NhaTroRepository = nhaTroRepository;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("MotelData");
        }

        public IActionResult Index()
        {
            CommonViewModel common = new CommonViewModel();
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            common.homeViewModel.TongPhong = NhaTroRepository.TongPhong(_nhaTro);
            common.homeViewModel.TongPhongTrong = NhaTroRepository.TongPhongTrong(_nhaTro);
            return View(common);

        }

    }
}