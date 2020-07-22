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
    public class DichVuController : Controller
    {
        private readonly IDichVuRepository Repository = null;
        private readonly INhaTroRepository NhaTroRepository = null;
        private readonly ILoaiDichVuRepository LoaiDVRepository = null;
        private readonly IDonViTinhRepository DonViRepository = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int _nhaTro = 0;
        private string _taikhoan = string.Empty;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;

        public DichVuController(IPhanQuyenRepository phanQuyenRepository, IHttpContextAccessor httpContextAccessor, IDichVuRepository repository, IDonViTinhRepository donViRepository, ILoaiDichVuRepository loaiDVRepository, INhaTroRepository nhaTroRepository)
        {
            this.Repository = repository;
            this.LoaiDVRepository = loaiDVRepository;
            this.DonViRepository = donViRepository;
            this.NhaTroRepository = nhaTroRepository;
            this._httpContextAccessor = httpContextAccessor;
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("MotelData");
            this.PhanQuyenRepository = phanQuyenRepository;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");

        }
        public IActionResult Index()
        {
            CommonViewModel model = new CommonViewModel();
            model.dichVuViewModel.listDichVu = Repository.GetsByNhaTro(_nhaTro);
            model.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, DichVuViewModel loai)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in loai.listLoaiDichVu)
                {
                    if (item.IsCheck == true)
                    {
                        LoaiDichVuView i = this.Repository.GetsByIdMaLoaiDV(item.MaLoaiDV);
                        DichVu dv = new DichVu();
                        dv.Ten = i.TenLoaiDV;
                        dv.Gia = i.DonGia;
                        dv.MoTa = i.Mota;
                        dv._MaDVT = i._MaDVi;
                        dv._MaLDV = i.MaLoaiDV;
                        dv._MaNT = loai.dichVu._MaNT;
                        dv.MacDinh = i.MacDinh;
                        await Repository.Create(dv);
                    }
                }
                CommonViewModel model = new CommonViewModel();
                model.dichVuViewModel.listDichVu = Repository.GetsByNhaTro(_nhaTro);
                model.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", loai.dichVu) });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DichVuViewModel loai)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    loai.dichVu.MaDV = id;
                    await Repository.Update(loai.dichVu);
                }
                catch
                {
                    throw;
                }
                CommonViewModel model = new CommonViewModel();
                model.dichVuViewModel.listDichVu = Repository.GetsByNhaTro(_nhaTro);
                model.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Edit", loai.dichVu) });
        }

        [HttpGet]
        public IActionResult AddOrEdit(int id)
        {
            DichVuViewModel model = new DichVuViewModel();
            model.listLoaiDichVu = LoaiDVRepository.GetListByMaNhaTroByDichVu(_nhaTro);
            model.listNhaTro = NhaTroRepository.Gets().Where(t => t.MaNT == _nhaTro);
            model.dichVu = new DichVu();
            model.dichVu._MaNT = _nhaTro;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            DichVuViewModel model = new DichVuViewModel();
            model.listNhaTro = NhaTroRepository.Gets().Where(t => t.MaNT == _nhaTro);
            model.listDonViTinh = DonViRepository.Gets();
            model.dichVu = await Repository.GetsById(id);
            if (model.dichVu == null)
                return NotFound();
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CommonViewModel model = new CommonViewModel();

            model.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            if (id == 0)
            {
                model.dichVuViewModel.listDichVu = Repository.GetsByNhaTro(_nhaTro);
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int checkForeign = Repository.CheckForeignKey(id);
                if (checkForeign == 1)
                {
                    int kq = await Repository.Delete(id);
                    if (kq == 0)
                        return NotFound();
                    model.dichVuViewModel.listDichVu = Repository.GetsByNhaTro(_nhaTro);
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
                }
                else
                {
                    model.dichVuViewModel.listDichVu = Repository.GetsByNhaTro(_nhaTro);
                    return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
                }
            }
        }
    }
}