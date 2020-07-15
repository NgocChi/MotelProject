using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;
using Web;

namespace Motel.Controllers
{
    public class PhongTroController : Controller
    {
        private readonly IPhongRepository Repository = null;
        private readonly INhaTroRepository NhaTroRepository = null;
        private readonly ILoaiPhongRepository LoaiPhongRepository = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int _nhaTro = 0;
        public PhongTroController(IPhongRepository repository, INhaTroRepository nhaTroRepository, ILoaiPhongRepository loaiPhongRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.Repository = repository;
            this.NhaTroRepository = nhaTroRepository;
            this.LoaiPhongRepository = loaiPhongRepository;
            _httpContextAccessor = httpContextAccessor;
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("UserData");
        }
        public ViewResult Index()
        {
            QuanLyPhongViewModel model = new QuanLyPhongViewModel();
            model.listPhong = Repository.Gets(_nhaTro);
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
                model.listPhong = Repository.Gets(_nhaTro);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", ph.phong) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            QuanLyPhongViewModel model = new QuanLyPhongViewModel();
            model.listNhaTro = NhaTroRepository.Gets().Where(t => t.MaNT == _nhaTro);
            model.listTrangThaiPhong = Repository.GetsTrangThaiPhong();
            model.listLoaiPhong = LoaiPhongRepository.GetsLoaiPhong();

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
        public async Task<IActionResult> Delete(int id)
        {
            QuanLyPhongViewModel model = new QuanLyPhongViewModel();
            if (id == 0)
            {
                model.listPhong = Repository.Gets();
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
                    model.listPhong = Repository.Gets(_nhaTro);
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
                }
                else
                {
                    model.listPhong = Repository.Gets(_nhaTro);
                    return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
                }
            }
        }



    }
}