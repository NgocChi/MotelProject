using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Data
{
    public class AppDBContext : IdentityDbContext<IdentityUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }

        public DbSet<TrangThaiPhong> TrangThaiPhongs { get; set; }

        public DbSet<PhuongTien> PhuongTiens { get; set; }

        public DbSet<Phong> Phongs { get; set; }

        public DbSet<PhieuThanhToan> PhieuThanhToans { get; set; }
        public DbSet<NhaTro> NhaTros { get; set; }
        public DbSet<LoaiPhong> LoaiPhongs { get; set; }

        public DbSet<LoaiHoaDon> LoaiHoaDons { get; set; }

        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }

        public DbSet<DienNuoc> DienNuocs { get; set; }

        public DbSet<DichVuPhong> DichVuPhongs { get; set; }

        public DbSet<DichVu> DichVus { get; set; }

        public DbSet<DatPhong> DatPhongs { get; set; }

        public DbSet<ChuTro> ChuTros { get; set; }

        public DbSet<DonViTinh> DonViTinhs { get; set; }

        public DbSet<LoaiDichVu> LoaiDichVus { get; set; }

        public DbSet<ChiTietPhieuThanhToan> ChiTietPhieuThanhToans { get; set; }

        public DbSet<ManHinh> ManHinhs { get; set; }

        public DbSet<PhanQuyen> PhanQuyens { get; set; }

        public DbSet<NhomNguoiDung> NhomNguoiDungs { get; set; }
    }

}
