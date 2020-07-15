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
    public class HopDongController : Controller
    {
        private readonly IHopDongRepository Repository = null;
        private readonly IPhongRepository PhongRepository = null;
        private readonly IKhachHangRepository KhachHangRepository = null;
        private readonly IDichVuRepository DichVuRepository = null;
        private readonly IChuTroRepository ChuTroRepository = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDichVuPhongRepository DichVuPhongRepository = null;
        private readonly IDatPhongRepository DatPhongRepository = null;
        private int _nhaTro = 0;

        public HopDongController(IHttpContextAccessor httpContextAccessor, IDatPhongRepository datPhongRepository, IDichVuPhongRepository dichVuPhongRepository, IChuTroRepository chuTroRepository, IHopDongRepository repository, IPhongRepository phongRepository, IKhachHangRepository khachHangRepository, IDichVuRepository dichVuRepository)
        {
            this.Repository = repository;
            this.PhongRepository = phongRepository;
            this.KhachHangRepository = khachHangRepository;
            this.DichVuRepository = dichVuRepository;
            this._httpContextAccessor = httpContextAccessor;
            this.ChuTroRepository = chuTroRepository;
            this.DichVuPhongRepository = dichVuPhongRepository;
            this.DatPhongRepository = datPhongRepository;
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("UserData");
        }
        public IActionResult Index()
        {
            QuanLyHopDongViewModel hd = new QuanLyHopDongViewModel();
            hd.listHopDong = Repository.Gets();
            return View(hd);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, int idDatPhong, QuanLyHopDongViewModel hopdong)
        {
            int kq = -1;
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    kq = await Repository.Create(hopdong.hopDongKhachHangPhong.hopDong);
                    foreach (var item in hopdong.listDichVu)
                    {
                        if (item.IsCheck == true)
                        {
                            DichVuPhong dvp = new DichVuPhong();
                            dvp._MaDV = item.MaDV;
                            dvp._MaPH = hopdong.hopDongKhachHangPhong.hopDong._MaPH;
                            await DichVuPhongRepository.Create(dvp);
                        }
                    }
                    await PhongRepository.UpdateTTP(hopdong.hopDongKhachHangPhong.hopDong._MaPH, 3);
                }
                else
                {
                    try
                    {
                        hopdong.hopDongKhachHangPhong.hopDong.MaHopDong = id;
                        kq = await Repository.Update(hopdong.hopDongKhachHangPhong.hopDong);
                    }
                    catch
                    {
                        throw;
                    }
                }
                QuanLyHopDongViewModel model = new QuanLyHopDongViewModel();
                model.listHopDong = Repository.Gets();
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", hopdong) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id, int idDatPhong)
        {
            IActionResult result;
            QuanLyHopDongViewModel model = new QuanLyHopDongViewModel();
            model.listPhong = PhongRepository.GetsPTrong(_nhaTro);
            model.listKhachHang = KhachHangRepository.Gets();
            model.listDichVu = DichVuRepository.GetsByNhaTro(_nhaTro);
            model.listKHDestination = new List<KhachHang>();
            model.listChuTro = ChuTroRepository.Gets();
            model.listDichVuDestination = new List<DichVu>();
            model.hopDongKhachHangPhong = new HopDongKhachHang();
            if (id == 0)
            {
                model.hopDongKhachHangPhong.hopDong = new HopDong();
                model.hopDongKhachHangPhong.dichVuPhong = new DichVuPhong();
                if (idDatPhong != 0)
                {
                    model.hopDongKhachHangPhong.datPhong = new DatPhong();
                    model.hopDongKhachHangPhong.datPhong = await DatPhongRepository.GetsById(idDatPhong);
                    model.hopDongKhachHangPhong.hopDong._MaPH = model.hopDongKhachHangPhong.datPhong._MaPH;
                }

                result = View(model);
            }
            else
            {
                model.hopDongKhachHangPhong.hopDong = await Repository.GetById(id);
                if (model.hopDongKhachHangPhong.hopDong == null)
                    result = NotFound();
                result = Json(model);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            QuanLyHopDongViewModel model = new QuanLyHopDongViewModel();
            if (id == 0)
            {
                model.listHopDong = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                model.listHopDong = Repository.Gets();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
        }

    }
}