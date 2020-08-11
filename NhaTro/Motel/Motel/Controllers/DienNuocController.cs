using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;
using Web;

namespace Motel.Controllers
{
    public class DienNuocController : Controller
    {

        private readonly IPhongRepository Repository = null;
        private readonly IDienNuocRepository DienNuocRepository = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int _nhaTro = 0;
        private string _taikhoan = string.Empty;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;
        public DienNuocController(IPhanQuyenRepository phanQuyenRepository, IDienNuocRepository dienNuocRepository, IPhongRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            this.Repository = repository;
            this.DienNuocRepository = dienNuocRepository;
            _httpContextAccessor = httpContextAccessor;
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("MotelData");
            this.PhanQuyenRepository = phanQuyenRepository;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");
        }
        public IActionResult Index()
        {
            CommonViewModel common = new CommonViewModel();
            common.qlDienNuocViewModel.ThangNam = DateTime.Now;
            common.qlDienNuocViewModel.listDienNuoc = DienNuocRepository.Gets(DateTime.Now, _nhaTro);
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            return View(common);
        }
        public IActionResult IndexChange(DateTime thangNam)

        {
            CommonViewModel common = new CommonViewModel();
            common.qlDienNuocViewModel.listDienNuoc = DienNuocRepository.Gets(thangNam, _nhaTro);
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            return Json(new { html = Helper.RenderRazorViewToString(this, "Table", common) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, QuanLyDienNuocViewModel ph)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    kq = await DienNuocRepository.Create(ph.dienNuoc);
                }
                else
                {
                    ph.dienNuoc.MaDienNuoc = id;
                    kq = await DienNuocRepository.UpdateDN(ph.dienNuoc);
                }
                CommonViewModel common = new CommonViewModel();
                common.qlDienNuocViewModel.ThangNam = DateTime.Now;
                common.qlDienNuocViewModel.listDienNuoc = DienNuocRepository.Gets(common.qlDienNuocViewModel.ThangNam, _nhaTro);
                common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", common) });

            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", ph.dienNuoc) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            QuanLyDienNuocViewModel model = new QuanLyDienNuocViewModel();
            model.listPhong = Repository.Gets(_nhaTro);


            if (id == 0)
            {
                model.dienNuoc = new DienNuoc();
                model.dienNuoc.NgayGhiSo = DateTime.Now;
                result = View(model);
            }
            else
            {
                model.dienNuoc = await DienNuocRepository.GetById(id);
                if (model.dienNuoc == null)
                    result = NotFound();
                result = View(model);
            }
            return result;
        }
    }
}