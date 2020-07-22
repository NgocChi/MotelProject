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
    public class NhaTroController : Controller
    {
        private readonly INhaTroRepository Repository = null;
        private readonly ITaiKhoanRepository TaiKhoanRepository = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _taikhoan = string.Empty;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;
        public NhaTroController(IPhanQuyenRepository phanQuyenRepository, INhaTroRepository repository, IHttpContextAccessor httpContextAccessor, ITaiKhoanRepository taiKhoanRepository)
        {
            this.Repository = repository;
            this.TaiKhoanRepository = taiKhoanRepository;
            _httpContextAccessor = httpContextAccessor;
            this.PhanQuyenRepository = phanQuyenRepository;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");
        }

        public ViewResult Index()
        {
            CommonViewModel common = new CommonViewModel();
            common.nhaTroViewModel.listNhaTro = Repository.GetsList(_taikhoan);
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            return View(common);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Ten", "DiaChi", "TongPhong", "PhongTrong", "Mota")] NhaTro nhaTroViewModel)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    int _maChuTro = TaiKhoanRepository.GetByTaiKhoan(_taikhoan).MaChuTro;
                    nhaTroViewModel._MaChuTro = _maChuTro;
                    kq = await Repository.Create(nhaTroViewModel);
                }
                else
                {
                    try
                    {
                        nhaTroViewModel.MaNT = id;
                        kq = await Repository.Update(nhaTroViewModel);
                    }
                    catch
                    {
                        throw;
                    }
                }
                CommonViewModel nt = new CommonViewModel();
                nt.nhaTroViewModel.listNhaTro = Repository.GetsList(_taikhoan);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", nt) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", nhaTroViewModel) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            if (id == 0)
            {
                return View(new NhaTro());
            }
            else
            {
                var kq = await Repository.GetsById(id);
                if (kq == null)
                    result = NotFound();
                result = View(kq);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CommonViewModel nt = new CommonViewModel();

            if (id == 0)
            {
                nt.nhaTroViewModel.listNhaTro = Repository.GetsList(_taikhoan);
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", nt) });
            }
            else
            {
                int checkForeign = Repository.CheckForeignKey(id);
                if (checkForeign == 1)
                {
                    int kq = await Repository.Delete(id);
                    if (kq == 0)
                        return NotFound();
                    nt.nhaTroViewModel.listNhaTro = Repository.GetsList(_taikhoan);
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", nt) });
                }
                else
                {
                    nt.nhaTroViewModel.listNhaTro = Repository.GetsList(_taikhoan);
                    return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "ViewAll", nt) });
                }
            }
        }
    }
}
