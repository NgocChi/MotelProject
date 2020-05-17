using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("ChuTro")]
    public class ChuTro
    {
        [Key]
        [Column("MaChuTro")]
        public int MaChuTro { get; set; }

        [Column("HoTen")]
        public string HoTen { get; set; }

        [Column("GioiTinh")]
        public int GioiTinh { get; set; }

        [Column("HinhDaiDien")]
        public string HinhDaiDien { get; set; }

        [Column("DiaChi")]
        public string DiaChi { get; set; }

        [Column("SoDienThoai")]
        public string SoDienThoai { get; set; }

        [Column("NgaySinh")]
        public DateTime NgaySinh { get; set; }

        [Column("Mail")]
        public string Mail { get; set; }

        [Column("_TenTaiKhoan")]
        public string _TenTaiKhoan { get; set; }
    }
}
