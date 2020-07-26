using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("HopDong")]
    public class HopDong
    {
        [Key]
        [Column("MaHopDong")]
        public int MaHopDong { get; set; }

        [Column("NgayBatDau")]
        public DateTime? NgayBatDau { get; set; }

        [Column("NgayKetThuc")]
        public DateTime? NgayKetThuc { get; set; }

        [Column("GiaPhong")]
        public decimal GiaPhong { get; set; }

        [Column("TienDatCoc")]
        public decimal TienDatCoc { get; set; }

        [Column("_MaKH")]
        public int _MaKH { get; set; }

        [Column("_MaCT")]
        public int _MaCT { get; set; }

        [Column("_MaPH")]
        public int _MaPH { get; set; }

        [Column("GhiChu")]
        public string GhiChu { get; set; }

        [Column("SoDien")]
        public int? SoDien { get; set; }

        [Column("SoNuoc")]
        public int? SoNuoc { get; set; }

        [Column("TrangThaiHD")]
        public bool? TrangThaiHD { get; set; }




    }
}
