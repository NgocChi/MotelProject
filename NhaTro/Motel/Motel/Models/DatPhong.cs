using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("DatPhong")]
    public class DatPhong
    {
        [Key]
        [Column("MaDP")]
        public int MaDP { get; set; }

        [Column("NgayDat")]
        public DateTime? NgayDat { get; set; }

        [Column("NgayHetHan")]
        public DateTime? NgayHetHan { get; set; }

        [Column("SoTienCoc")]
        public decimal SoTienCoc { get; set; }

        [Column("_MaPH")]
        public int _MaPH { get; set; }

        [Column("_MaKH")]
        public int? _MaKH { get; set; }

        [Column("GhiChu")]
        public string GhiChu { get; set; }
    }
}
