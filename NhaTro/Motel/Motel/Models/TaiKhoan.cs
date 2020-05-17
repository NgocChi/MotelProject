using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        [Column("TenTaiKhoan")]
        public string TenTaiKhoan { get; set; }

        [Column("MatKhau")]
        public string MatKhau { get; set; }

        [Column("GhiChu")]
        public string GhiChu { get; set; }

        [Column("LoaiTaiKhoan")]
        public int LoaiTaiKhoan { get; set; }
    }
}
