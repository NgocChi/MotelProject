﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("HopDong")]
    public class HopDong
    {
        [Key]
        [Column("MaHopDong")]
        public int MaHopDong { get; set; }

        [Column("NgayBatDau")]
        public DateTime NgayBatDau { get; set; }

        [Column("NgayKetThuc")]
        public DateTime NgayKetThuc { get; set; }

        [Column("GiaPhong")]
        public double GiaPhong { get; set; }

        [Column("TienDatCoc")]
        public double TienDatCoc { get; set; }

        [Column("_MaKH")]
        public int _MaKH { get; set; }

        [Column("_MaCT")]
        public int _MaCT { get; set; }

        [Column("_MaPH")]
        public int _MaPH { get; set; }
    }
}
