using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;

namespace Motel.Controllers
{
    public class DichVuController : Controller
    {
        private readonly IDichVuRepository Repository = null;
        private readonly INhaTroRepository NhaTroRepository = null;
        private readonly ILoaiDichVuRepository LoaiDVRepository = null;
        private readonly IDonViTinhRepository DonViRepository = null;

        public DichVuController(IDichVuRepository repository, IDonViTinhRepository donViRepository, ILoaiDichVuRepository loaiDVRepository, INhaTroRepository nhaTroRepository)
        {
            this.Repository = repository;
            this.LoaiDVRepository = loaiDVRepository;
            this.DonViRepository = donViRepository;
            this.NhaTroRepository = nhaTroRepository;

        }
        public IActionResult Index()
        {
            DichVuViewModel model = new DichVuViewModel();
            model.listDichVu = Repository.Gets();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, DichVuViewModel loai)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    foreach (var item in loai.listLoaiDichVu)
                    {
                        if (item.IsCheck == true)
                        {
                            LoaiDichVuViewModel1 i = this.Repository.GetsByIdMaLoaiDV(item.MaLoaiDV);
                            loai.dichVu.Ten = i.TenLoaiDV;
                            loai.dichVu.Gia = i.DonGia;
                            loai.dichVu.MoTa = i.Mota;
                            loai.dichVu._MaDVT = i._MaDVi;
                            loai.dichVu._MaLDV = i.MaLoaiDV;
                            await Repository.Create(loai.dichVu);
                        }
                    }

                }
                else
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
                }
                DichVuViewModel model = new DichVuViewModel();
                model.listDichVu = Repository.Gets();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", loai.dichVu) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            DichVuViewModel model = new DichVuViewModel();
            model.listLoaiDichVu = LoaiDVRepository.GetList();
            model.listNhaTro = NhaTroRepository.Gets();
            if (id == 0)
            {
                model.dichVu = new DichVu();
                result = View(model);
            }
            else
            {
                model.dichVu = await Repository.GetsById(id);
                if (model.dichVu == null)
                    result = NotFound();
                result = View(model);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            DichVuViewModel model = new DichVuViewModel();
            if (id == 0)
            {
                model.listDichVu = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                model.listDichVu = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });

            }
        }
    }
}