using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("PhanQuyen")]
    public class PhanQuyen
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("MaNhomNguoiDung")]
        public int MaNhomNguoiDung { get; set; }

        [Column("MaManHinh")]
        public int MaManHinh { get; set; }

        [Column("CoQuyen")]
        public bool CoQuyen { get; set; }
    }
}
