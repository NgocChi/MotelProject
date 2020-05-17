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
        [Column("MaPH")]
        public int MaPH { get; set; }

        [Column("NgayVao")]
        public DateTime NgayVao { get; set; }

        [Column("SoTien")]
        public double SoTien { get; set; }

        [Column("_MaPH")]
        public int _MaPH { get; set; }

        [Column("_MaKH")]
        public int _MaKH { get; set; }
    }
}
