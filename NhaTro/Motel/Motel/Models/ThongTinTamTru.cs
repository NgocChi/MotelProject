using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models
{
    [Table("ThongTinTamTru")]
    public class ThongTinTamTru
    {
        [Key]
        [Column("MaTT")]
        public int MaTT { get; set; }

        [Column("HoTen")]
        public string HoTen { get; set; }  //1

        [Column("TenGoiKhac")]
        public string TenGoiKhac { get; set; }  //3

        [Column("NgaySinh")]
        public DateTime NgaySinh { get; set; }  //2

        [Column("GioiTinh")]
        public string GioiTinh { get; set; }  //4

        [Column("NoiSinh")]
        public string NoiSinh { get; set; } //5

        [Column("NguyenQuan")]
        public string NguyenQuan { get; set; } //6

        [Column("DanToc")]
        public string DanToc { get; set; } //7

        [Column("TonGiao")]
        public string TonGiao { get; set; } //8

        [Column("QuocTich")]
        public string QuocTich { get; set; } //9

        [Column("CMND")]
        public string CMND { get; set; } //10.1

        [Column("HoChieuSo")]
        public string HoChieuSo { get; set; } //11

        [Column("NoiThuongTru")]
        public string NoiThuongTru { get; set; } //12

        [Column("NoiTamTru")]
        public string NoiTamTru { get; set; } //13

        [Column("TrinhDoHocVan")]
        public string TrinhDoHocVan { get; set; } //14

        [Column("TrinhDoChuyenMon")]
        public string TrinhDoChuyenMon { get; set; } //15

        [Column("TiengDanToc")]
        public string TiengDanToc { get; set; } //16

        [Column("TrinhDoNgoaiNgu")]
        public string TrinhDoNgoaiNgu { get; set; } //17

        [Column("NgheNghiep")]
        public string NgheNghiep { get; set; }  //18

        [Column("NoiLamViec")]
        public string NoiLamViec { get; set; }  //19


        [Column("TomTatBanThan")]
        public string TomTatBanThan { get; set; }  //18

        [Column("TomTatGiaDinh")]
        public string TomTatGiaDinh { get; set; }

        [Column("TinhTrangHonNhan")]
        public string TinhTrangHonNhan { get; set; } //11
    }
}
