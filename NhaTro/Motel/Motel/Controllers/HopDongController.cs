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
        public IActionResult Index1(int trangThai = 0)
        {
            QuanLyHopDongViewModel hd = new QuanLyHopDongViewModel();
            switch (trangThai)
            {
                case 0:
                    hd.listHopDong = Repository.Gets(_nhaTro);
                    break;
                case 1:
                    hd.listHopDong = Repository.Gets(_nhaTro).Where(t => t.NgayKetThuc >= DateTime.Now);
                    break;
                case 2:
                    hd.listHopDong = Repository.Gets(_nhaTro).Where(t => t.NgayKetThuc < DateTime.Now);
                    break;

            }
            return Json(new { html = Helper.RenderRazorViewToString(this, "Table", hd) });
        }


        public IActionResult Index()
        {
            QuanLyHopDongViewModel hd = new QuanLyHopDongViewModel();
            hd.listHopDong = Repository.Gets(_nhaTro);
            return View(hd);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, int idDatPhong, QuanLyHopDongViewModel hopdong)
        {
            int kq = -1;
            try
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
                            dvp._MaHD = kq;
                            await DichVuPhongRepository.Create(dvp);
                        }
                    }
                    await PhongRepository.UpdateTTP(hopdong.hopDongKhachHangPhong.hopDong._MaPH, 3);
                }
                else
                {

                    hopdong.hopDongKhachHangPhong.hopDong.MaHopDong = id;
                    kq = await Repository.Update(hopdong.hopDongKhachHangPhong.hopDong);
                    foreach (var item in hopdong.listDichVu)
                    {
                        if (item.IsCheck == true)
                        {

                            DichVuPhong dvp = new DichVuPhong();
                            dvp._MaDV = item.MaDV;
                            dvp._MaPH = hopdong.hopDongKhachHangPhong.hopDong._MaPH;
                            dvp._MaHD = id;
                            dvp.SoLuong = item.SoLuong;
                            int check = DichVuPhongRepository.CheckExist(dvp);
                            if (check == 1)
                                await DichVuPhongRepository.Create(dvp);
                            else
                                await DichVuPhongRepository.Update(dvp);

                        }
                        else
                        {
                            DichVuPhong dvp = new DichVuPhong();
                            dvp._MaDV = item.MaDV;
                            dvp._MaPH = hopdong.hopDongKhachHangPhong.hopDong._MaPH;
                            dvp._MaHD = id;
                            dvp.SoLuong = item.SoLuong;
                            int check = DichVuPhongRepository.CheckExist(dvp);
                            if (check == 0)
                                await DichVuPhongRepository.Delete(dvp);
                        }
                    }

                }
                QuanLyHopDongViewModel model = new QuanLyHopDongViewModel();
                model.listHopDong = Repository.Gets(_nhaTro);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            catch
            {
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", hopdong) });
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id, int idDatPhong, int idPhong = 0)
        {
            IActionResult result;
            QuanLyHopDongViewModel model = new QuanLyHopDongViewModel();
            model.listKhachHang = KhachHangRepository.Gets();
            model.listDichVu = DichVuRepository.GetsByNhaTro(_nhaTro, idPhong);
            model.listChuTro = ChuTroRepository.Gets();
            model.hopDongKhachHangPhong = new HopDongKhachHang();
            model.hopDongKhachHangPhong.hopDong = new HopDong();
            model.hopDongKhachHangPhong.dichVuPhong = new DichVuPhong();
            if (id == 0) // nếu thêm mới hợp đồng
            {
                if (idDatPhong != 0) // nếu đã đặt phòng
                {
                    model.hopDongKhachHangPhong.datPhong = new DatPhongViewModel();
                    model.hopDongKhachHangPhong.datPhong = DatPhongRepository.GetsByIdDP(idDatPhong);
                    model.listPhong = PhongRepository.GetsPTrong(_nhaTro, model.hopDongKhachHangPhong.datPhong._MaPH);
                    model.hopDongKhachHangPhong.hopDong._MaPH = model.hopDongKhachHangPhong.datPhong._MaPH;
                    model.hopDongKhachHangPhong.hopDong._MaKH = model.hopDongKhachHangPhong.datPhong._MaKH;
                    model.hopDongKhachHangPhong.hopDong.GiaPhong = model.hopDongKhachHangPhong.datPhong.Gia;
                    model.hopDongKhachHangPhong.hopDong.TienDatCoc = model.hopDongKhachHangPhong.datPhong.GiaDatCoc;
                    model.TongTien = model.hopDongKhachHangPhong.datPhong.GiaDatCoc + model.hopDongKhachHangPhong.datPhong.Gia - model.hopDongKhachHangPhong.datPhong.SoTienCoc;
                }
                else
                {
                    model.listPhong = PhongRepository.GetsPTrong(_nhaTro, 0);
                }

                result = View(model);
            }
            else // nếu edit hợp đồng
            {
                model.listPhong = PhongRepository.GetsPTrong(_nhaTro, idPhong);
                model.hopDongKhachHangPhong.hopDong = await Repository.GetById(id);
                if (model.hopDongKhachHangPhong.hopDong == null)
                    result = NotFound();
                result = View(model);
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
                model.listHopDong = Repository.Gets(_nhaTro);
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                model.listHopDong = Repository.Gets(_nhaTro);
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", model) });
            }
        }

    }
}