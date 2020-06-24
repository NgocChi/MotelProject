using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("Phong")]
    public class Phong
    {
        [Key]
        [Column("MaPH")]
        public int MaPH { get; set; }

        [Column("Ten")]
        public string Ten { get; set; }

        [Column("Tang")]
        public int Tang { get; set; }

        [Column("CSDien")]
        public Int64 CSDien { get; set; }

        [Column("CSNuoc")]
        public Int64 CSNuoc { get; set; }

        [Column("_MaNT")]
        public int _MaNT { get; set; }

        [Column("_MaLP")]
        public int? _MaLP { get; set; }

        [Column("_MaTTPH")]
        public int? _MaTTPH { get; set; }

        [Column("SoNguoiToiDa")]
        public int? SoNguoiToiDa { get; set; }


    }
}
