using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("TrangThaiPhong")]
    public class TrangThaiPhong
    {
        [Key]
        [Column("MaTTPH")]
        public int MaTTPH { get; set; }

        [Column("Ten")]
        public string Ten { get; set; }
    }
}
