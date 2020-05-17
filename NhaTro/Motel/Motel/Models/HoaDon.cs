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

        [Column("SoLuong")]
        public int SoLuong { get; set; }

        [Column("DonGia")]
        public double DonGia { get; set; }

        [Column("ThanhTien")]
        public double ThanhTien { get; set; }

        [Column("DaThanhToan")]
        public int DaThanhToan { get; set; }

        [Column("_MaPH")]
        public int _MaPH { get; set; }

        [Column("_MaDVPH")]
        public int _MaDVPH { get; set; }

        [Column("_MaLoaiHD")]
        public int _MaLoaiHD { get; set; }
    }
}
