using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("PhieuThanhToan")]
    public class PhieuThanhToan
    {
        [Key]
        [Column("MaThanhToan")]
        public int MaThanhToan { get; set; }

        [Column("NgayLap")]
        public DateTime NgayLap { get; set; }

        [Column("TongTien")]
        public double TongTien { get; set; }

        [Column("NoiDung")]
        public string NoiDung { get; set; }

        [Column("_MaChuTro")]
        public int _MaChuTro { get; set; }

        [Column("_MaKH")]
        public int _MaKH { get; set; }




    }
}
