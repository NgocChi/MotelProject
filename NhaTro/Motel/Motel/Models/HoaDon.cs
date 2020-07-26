using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        [Column("MaHD")]
        public int MaHD { get; set; }

        [Column("NgayLap")]
        public DateTime NgayLap { get; set; }

        [Column("ThanhTien")]
        public decimal? ThanhTien { get; set; }

        [Column("DaThanhToan")]
        public bool? DaThanhToan { get; set; }

        [Column("_MaPH")]
        public int? _MaPH { get; set; }


        [Column("_MaLoaiHD")]
        public int? _MaLoaiHD { get; set; }


        [Column("_MaHD")]
        public int? _MaHD { get; set; }

        [Column("TrangThai")]
        public bool? TrangThai { get; set; }

        [Column("NgayThanhToan")]
        public DateTime? NgayThanhToan { get; set; }
    }
}
