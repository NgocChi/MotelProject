using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("LoaiDichVu")]
    public class LoaiDichVu
    {
        [Key]
        [Column("MaLoaiDV")]
        public int MaLoaiDV { get; set; }


        [Column("TenLoaiDV")]
        public string TenLoaiDV { get; set; }

        [Column("_MaDVi")]
        public int _MaDVi { get; set; }

        [Column("DonGia")]
        public decimal DonGia { get; set; }

        [Column("Mota")]
        public string Mota { get; set; }

        [Column("MacDinh")]
        public bool? MacDinh { get; set; }
    }
}
