using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("ChiTietPhieuThanhToan")]
    public class ChiTietPhieuThanhToan
    {
        [Key]
        [Column("_MaHD")]
        public int _MaHD { get; set; }

        [Column("_MaThanhToan")]
        public int _MaThanhToan { get; set; }
    }
}
