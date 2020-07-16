using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("NhomNguoiDung")]
    public class NhomNguoiDung
    {
        [Key]
        [Column("MaNhomNguoiDung")]
        public int MaNhomNguoiDung { get; set; }

        [Column("TenNhomNguoiDung")]
        public string TenNhomNguoiDung { get; set; }
    }
}
