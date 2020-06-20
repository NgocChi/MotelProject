using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.ViewModels;

namespace Motel.Controllers
{
    public class HopDongController : Controller
    {
        private readonly IHopDongRepository Repository = null;
        private readonly IPhongRepository PhongRepository = null;
        private readonly IKhachHangRepository KhachHangRepository = null;
        public HopDongController(IHopDongRepository repository, IPhongRepository phongRepository, IKhachHangRepository khachHangRepository)
        {
            this.Repository = repository;
            this.PhongRepository = phongRepository;
            this.KhachHangRepository = khachHangRepository;
        }
        public IActionResult Index()
        {
            QuanLyHopDongViewModel hd = new QuanLyHopDongViewModel();
            hd.listHopDong = Repository.Gets();
            hd.listPhong = PhongRepository.GetsPhongTrong();
            hd.listKhachHang = KhachHangRepository.Gets();
            return View(hd);
        }
    }
}