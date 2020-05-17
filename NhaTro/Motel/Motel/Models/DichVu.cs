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
        public int MaHopDong { get; set; }

        [Column("Ten")]
        public string Ten { get; set; }

        [Column("Gia")]
        public double Gia { get; set; }

        [Column("_MaNT")]
        public int _MaNT { get; set; }
    }
}
