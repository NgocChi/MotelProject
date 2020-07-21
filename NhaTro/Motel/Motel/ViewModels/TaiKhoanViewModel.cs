using Motel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
    public class QuanLyTaiKhoan
    {
        public IEnumerable<NhaTro> listNhaTro { get; set; }

        public IEnumerable<TaiKhoan> listTaiKhoan { get; set; }

        public TaiKhoanViewModel taikhoan { get; set; }

        public ChuTro chuTro { get; set; }

        public int _chooseMotel { get; set; }
    }

    public class TaiKhoanViewModel
    {
        public string TenTaiKhoan { get; set; }

        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Remember Me")]
        public bool Nho { get; set; }

        public string ReturnUrl { get; set; }

        public string PrMatKhau { get; set; }

    }
}
