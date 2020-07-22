using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("ManHinh")]
    public class ManHinh
    {
        [Key]
        [Column("MaManHinh")]
        public int MaManHinh { get; set; }

        [Column("TenManHinh")]
        public string TenManHinh { get; set; }

        [Column("KeyControl")]
        public string KeyControl { get; set; }

        [Column("ParentId")]
        public int ParentId { get; set; }

        [Column("Controller")]
        public string Controller { get; set; }
    }
}
