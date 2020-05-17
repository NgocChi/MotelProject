using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("PhuongTien")]
    public class PhuongTien
    {
        [Key]
        [Column("MaPT")]
        public int MaPT { get; set; }

        [Column("Ten")]
        public string Ten { get; set; }

        [Column("BienSo")]
        public string BienSo { get; set; }

        [Column("_MaHopDong")]
        public int _MaHopDong { get; set; }

        [Column("HinhAnh")]
        public string HinhAnh { get; set; }
    }
}
