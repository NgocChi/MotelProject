using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("ChiTietHoaDon")]
    public class ChiTietHoaDon
    {
        [Key]
        [Column("MaCTHD")]
        public int MaCTHD { get; set; }

        [Column("_MaDVP")]
        public int _MaDVP { get; set; }

        [Column("_MaHoaDon")]
        public int _MaHoaDon { get; set; }

        [Column("ThanhTien")]
        public decimal? ThanhTien { get; set; }

    }
}
