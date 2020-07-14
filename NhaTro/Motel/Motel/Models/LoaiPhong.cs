﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("LoaiPhong")]
    public class LoaiPhong
    {

        [Key]
        [Column("MaLP")]
        public int MaLP { get; set; }

        [Column("Ten")]
        public string Ten { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        [Column("Gia")]
        public decimal Gia { get; set; }

        [DataType(DataType.Currency)]
        [Column("GiaDatCoc")]
        public decimal GiaDatCoc { get; set; }

        [Column("ThongTin")]
        public string ThongTin { get; set; }

        [Column("DienTich")]
        public int DienTich { get; set; }
    }
}
