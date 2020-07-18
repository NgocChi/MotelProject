﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("DichVuPhong")]
    public class DichVuPhong
    {
        [Key]
        [Column("MaDVPH")]
        public int MaDVPH { get; set; }

        [Column("_MaDV")]
        public int _MaDV { get; set; }

        [Column("_MaPH")]
        public int _MaPH { get; set; }

        [Column("_MaHD")]
        public int? _MaHD { get; set; }

        [Column("SoLuong")]
        public int? SoLuong { get; set; }

        [Column("TrangThai")]
        public int? TrangThai { get; set; }
    }
}
