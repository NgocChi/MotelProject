using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("DienNuoc")]
    public class DienNuoc
    {
        [Key]
        [Column("MaDienNuoc")]
        public int MaDienNuoc { get; set; }

        [Column("NgayGhiSo")]
        public DateTime NgayGhiSo { get; set; }

        [Column("CSDienCu")]
        public int CSDienCu { get; set; }

        [Column("CSDienMoi")]
        public int CSDienMoi { get; set; }

        [Column("CSNuocCu")]
        public int CSNuocCu { get; set; }

        [Column("CSNuocMoi")]
        public int CSNuocMoi { get; set; }

        [Column("MaPH")]
        public int MaPH { get; set; }
    }
}
