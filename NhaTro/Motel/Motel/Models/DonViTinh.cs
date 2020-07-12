using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("DonViTinh")]
    public class DonViTinh
    {
        [Key]
        [Column("MaDonVi")]
        public int MaDonVi { get; set; }


        [Column("TenDonVi")]
        public string TenDonVi { get; set; }

        [Column("MoTa")]
        public string MoTa { get; set; }
    }
}
