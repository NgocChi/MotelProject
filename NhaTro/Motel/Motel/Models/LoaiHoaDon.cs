using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("LoaiHoaDon")]
    public class LoaiHoaDon
    {
        [Key]
        [Column("MaLoaiHD")]
        public int MaLoaiHD { get; set; }

        [Column("Ten")]
        public string Ten { get; set; }
    }
}
