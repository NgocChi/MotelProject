using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;

namespace Motel.Controllers
{
    public class PhongTroController : Controller
    {
        private readonly IPhongRepository Repository = null;
        private readonly INhaTroRepository NhaTroRepository = null;
        public PhongTroController(IPhongRepository repository, INhaTroRepository nhaTroRepository)
        {
            this.Repository = repository;
            this.NhaTroRepository = nhaTroRepository;
        }
        public ViewResult Index()
        {
            QuanLyPhongViewModel model = new QuanLyPhongViewModel();
            model.listPhong = Repository.Gets();
            model.listLoaiPhong = Repository.GetsLoaiPhong();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, QuanLyPhongViewModel ph)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    kq = await Repository.Create(ph.phong);
                    await NhaTroRepository.UpdateSoLuongPhong(ph.phong._MaNT, 1);
                }
                else
                {
                    try
                    {
                        ph.phong.MaPH = id;
                        kq = await Repository.Update(ph.phong);
                    }
                    catch
                    {
                        throw;
                    }
                }
                QuanLyPhongViewModel model = new QuanLyPhongViewModel();
                model.listPhong = Repository.Gets();
                model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", ph.phong) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            QuanLyPhongViewModel model = new QuanLyPhongViewModel();
            model.listLoaiPhong = Repository.GetsLoaiPhong();
            model.listNhaTro = NhaTroRepository.Gets();
            model.listTrangThaiPhong = Repository.GetsTrangThaiPhong();
            if (id == 0)
            {
                model.phong = new Phong();
                result = View(model);
            }
            else
            {
                model.phong = await Repository.GetById(id);
                if (model.phong == null)
                    result = NotFound();
                result = View(model);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditLoaiPh(int id, QuanLyPhongViewModel loaiph)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    kq = await Repository.CreateLoaiPhong(loaiph.loaiPhong);
                }
                else
                {
                    try
                    {
                        loaiph.loaiPhong.MaLP = id;
                        kq = await Repository.UpdateLoaiPhong(loaiph.loaiPhong);
                    }
                    catch
                    {
                        throw;
                    }
                }
                QuanLyPhongViewModel model = new QuanLyPhongViewModel();
                model.listPhong = Repository.Gets();
                model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", loaiph.loaiPhong) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEditLoaiPh(int id)
        {
            IActionResult result;
            QuanLyPhongViewModel model = new QuanLyPhongViewModel();
            if (id == 0)
            {
                model.loaiPhong = new LoaiPhong();
                result = View(model);
            }
            else
            {
                model.loaiPhong = await Repository.GetLoaiPhById(id);
                if (model.loaiPhong == null)
                    result = NotFound();
                result = View(model);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            QuanLyPhongViewModel model = new QuanLyPhongViewModel();
            if (id == 0)
            {
                model.listPhong = Repository.Gets();
                model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                model.listPhong = Repository.Gets();
                model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLoaiPh(int id)
        {
            QuanLyPhongViewModel model = new QuanLyPhongViewModel();
            if (id == 0)
            {
                model.listPhong = Repository.Gets();
                model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int kq = await Repository.DeleteLoaiPh(id);
                if (kq == 0)
                    return NotFound();
                model.listPhong = Repository.Gets();
                model.listLoaiPhong = Repository.GetsLoaiPhong();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });

            }
        }

    }
}