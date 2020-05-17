using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        [Column("MaKH")]
        public int MaKh { get; set; }

        [Column("TenKH")]
        public string TenKH { get; set; }

        [Column("GioiTinh")]
        public int GioiTinh { get; set; }

        [Column("HinhDaiDien")]
        public string HinhDaiDien { get; set; }

        [Column("QueQuan")]
        public string QueQuan { get; set; }

        [Column("SoDienThoai")]
        public string SoDienThoai { get; set; }

        [Column("NgaySinh")]
        public DateTime NgaySinh { get; set; }

        [Column("Mail")]
        public string Mail { get; set; }

        [Column("_TenTaiKhoan")]
        public string TenTaiKhoan { get; set; }

        [Column("_MaNguoiThan")]
        public int MaNguoiThan { get; set; }
    }
}
