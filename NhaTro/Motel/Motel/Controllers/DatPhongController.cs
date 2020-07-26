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
using Rotativa.AspNetCore;
using Web;

namespace Motel.Controllers
{
    public class DatPhongController : Controller
    {
        private readonly IDatPhongRepository Repository = null;
        private readonly IPhongRepository PhongRepository = null;
        private readonly IKhachHangRepository KhachHangRepository = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IChuTroRepository ChuTroRepository = null;
        private int _nhaTro = 0;
        private string _taikhoan = string.Empty;
        private readonly IPhanQuyenRepository PhanQuyenRepository = null;
        public DatPhongController(IChuTroRepository chuTroRepository, IPhanQuyenRepository phanQuyenRepository, IDatPhongRepository repository, IPhongRepository phongRepository, IKhachHangRepository khachHangRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.Repository = repository;
            this.ChuTroRepository = chuTroRepository;
            this.PhongRepository = phongRepository;
            this.KhachHangRepository = khachHangRepository;
            _httpContextAccessor = httpContextAccessor;
            _nhaTro = _httpContextAccessor.HttpContext.Session.GetComplexData<int>("MotelData");
            this.PhanQuyenRepository = phanQuyenRepository;
            _taikhoan = _httpContextAccessor.HttpContext.Session.GetComplexData<string>("UserData");
        }
        public IActionResult Index1(int trangThai = 0)
        {
            CommonViewModel common = new CommonViewModel();
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            switch (trangThai)
            {
                case 0:
                    common.qlDatPhongViewModel.listDatPhong = Repository.GetsByMaNhaTro(_nhaTro);
                    break;
                case 1:
                    common.qlDatPhongViewModel.listDatPhong = Repository.GetsByMaNhaTro(_nhaTro).Where(t => t.NgayHetHan >= DateTime.Now);
                    break;
                case 2:
                    common.qlDatPhongViewModel.listDatPhong = Repository.GetsByMaNhaTro(_nhaTro).Where(t => t.NgayHetHan < DateTime.Now);
                    break;

            }
            return Json(new { html = Helper.RenderRazorViewToString(this, "Table", common) });
        }

        public IActionResult Index()
        {
            CommonViewModel common = new CommonViewModel();
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
            common.qlDatPhongViewModel.listDatPhong = Repository.GetsByMaNhaTro(_nhaTro);
            return View(common);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, QuanLyDatPhongViewModel datPhong)
        {
            int kq = -1;
            CommonViewModel common = new CommonViewModel();
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    if (datPhong.khachHangDatPhong.datPhong._MaKH == 0)
                    {

                        //
                        datPhong.khachHangDatPhong.khachHang._MaNT = _nhaTro;
                        int makh = await KhachHangRepository.Create(datPhong.khachHangDatPhong.khachHang);
                        datPhong.khachHangDatPhong.datPhong._MaKH = makh;
                    }
                    await Repository.Create(datPhong.khachHangDatPhong.datPhong);
                    await PhongRepository.UpdateTTP(datPhong.khachHangDatPhong.datPhong._MaPH, 2);
                }
                else
                {
                    try
                    {
                        datPhong.khachHangDatPhong.datPhong.MaDP = id;
                        kq = await Repository.Update(datPhong.khachHangDatPhong.datPhong);
                        await PhongRepository.UpdateTTP(datPhong.khachHangDatPhong.datPhong._MaPH, 2);
                    }
                    catch
                    {
                        throw;
                    }
                }
                common.qlDatPhongViewModel.listDatPhong = Repository.GetsByMaNhaTro(_nhaTro);
                common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);
                return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
            }
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", datPhong) });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            IActionResult result;
            QuanLyDatPhongViewModel model = new QuanLyDatPhongViewModel();

            model.listKhachHang = KhachHangRepository.Gets().Where(t => t._MaNT == _nhaTro);
            model.listDatPhong = Repository.GetsByMaNhaTro(_nhaTro);
            if (id == 0)
            {
                model.listPhong = PhongRepository.GetsPhongTrong(0).Where(t => t._MaNT == _nhaTro);
                model.khachHangDatPhong = new KhachHangDatPhongViewModel();
                model.khachHangDatPhong.datPhong = new DatPhong();
                model.khachHangDatPhong.khachHang = new KhachHang();
                result = View(model);
            }
            else
            {
                model.khachHangDatPhong = new KhachHangDatPhongViewModel();
                model.khachHangDatPhong.datPhong = await Repository.GetsById(id);
                model.khachHangDatPhong.khachHang = await KhachHangRepository.GetsById(model.khachHangDatPhong.datPhong._MaKH);
                model.listPhong = PhongRepository.GetsPhongTrong(model.khachHangDatPhong.datPhong._MaPH);
                if (model.khachHangDatPhong.datPhong == null)
                    result = NotFound();
                result = View(model);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CommonViewModel common = new CommonViewModel();
            common.list = PhanQuyenRepository.GetsManHinhPhanQuyen(_taikhoan);

            if (id == 0)
            {
                common.qlDatPhongViewModel.listDatPhong = Repository.GetsByMaNhaTro(_nhaTro);
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", common) });
            }

            else
            {
                int kq = await Repository.Delete(id);
                if (kq == 0)
                    return NotFound();
                common.qlDatPhongViewModel.listDatPhong = Repository.GetsByMaNhaTro(_nhaTro);
                return Json(new { html = Helper.RenderRazorViewToString(this, "ViewAll", common) });

            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckIn(int id)
        {
            IActionResult result;
            QuanLyDatPhongViewModel model = new QuanLyDatPhongViewModel();
            model.listPhong = PhongRepository.GetsPhongTrong(id);
            model.listKhachHang = KhachHangRepository.Gets();
            model.listDatPhong = Repository.GetsByMaNhaTro(_nhaTro);
            if (id == 0)
            {
                model.khachHangDatPhong = new KhachHangDatPhongViewModel();
                model.khachHangDatPhong.datPhong = new DatPhong();
                model.khachHangDatPhong.khachHang = new KhachHang();
                result = View(model);
            }
            else
            {
                model.khachHangDatPhong = new KhachHangDatPhongViewModel();

                model.khachHangDatPhong.datPhong = await Repository.GetsById(id);
                model.khachHangDatPhong.khachHang = await KhachHangRepository.GetsById(model.khachHangDatPhong.datPhong._MaKH);
                if (model.khachHangDatPhong.datPhong == null)
                    result = NotFound();
                result = View(model);
            }
            return result;
        }

        public IActionResult ExportPDF(int id = 0)
        {
            ExportDatCocPhong dp = new ExportDatCocPhong();
            dp.chuTro = ChuTroRepository.GetByTK(_taikhoan);
            dp.datPhong = Repository.GetsByIdDP(id);
            return new ViewAsPdf("ExportPDF", dp)
            {

            };
        }

    }
}