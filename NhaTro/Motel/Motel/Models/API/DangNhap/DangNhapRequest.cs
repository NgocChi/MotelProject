using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models.API.DangNhap
{
    public class DangNhapRequest
    {
        [JsonProperty("TenTaiKhoan")]
        public string TenTaiKhoan { get; set; }

        [JsonProperty("MatKhau")]
        public string MatKhau { get; set; }
    }
}
