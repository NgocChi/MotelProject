using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("DichVu")]
    public class DichVu
    {
        [Key]
        [Column("MaDV")]
        public int MaDV { get; set; }

        [Column("Ten")]
        public string Ten { get; set; }

        [Column("Gia")]
        public decimal Gia { get; set; }

        [Column("MoTa")]
        public string MoTa { get; set; }

        [Column("_MaNT")]
        public int _MaNT { get; set; }

        [Column("_MaDVT")]
        public int _MaDVT { get; set; }

        [Column("_MaLDV")]
        public int _MaLDV { get; set; }

        [Column("MacDinh")]
        public bool? MacDinh { get; set; }
    }
}
