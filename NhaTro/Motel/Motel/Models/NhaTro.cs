using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("NhaTro")]
    public class NhaTro
    {
        [Key]
        [Column("MaNT")]
        public int MaNT { get; set; }

        [Column("Ten")]
        public string Ten { get; set; }

        [Column("TongPhong")]
        public int TongPhong { get; set; }

        [Column("PhongTrong")]
        public int PhongTrong { get; set; }

        [Column("DiaChi")]
        public string DiaChi { get; set; }

        [Column("Mota")]
        public string Mota { get; set; }

        [Column("_MaChuTro")]
        public int _MaChuTro { get; set; }

    }
}
