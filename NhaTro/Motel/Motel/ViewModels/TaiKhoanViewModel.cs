using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.ViewModels
{
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
