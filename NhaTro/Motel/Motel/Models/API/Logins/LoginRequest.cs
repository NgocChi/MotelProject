using Motel.Models.API.Bases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Models.API.Logins
{
    public class LoginRequest
    {
        [JsonProperty("TenTaiKhoan")]
        public string TenTaiKhoan { get; set; }

        [JsonProperty("MatKhau")]
        public string MatKhau { get; set; }
    }

    public class LoginResponse: ResponseBase
    {
        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("IdUser")]
        public int IdUser { get; set; }

        [JsonProperty("LoaiTaiKhoan")]
        public bool? LoaiTaiKhoan { get; set; }
    }
}
