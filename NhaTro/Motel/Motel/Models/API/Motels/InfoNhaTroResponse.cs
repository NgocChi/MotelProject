using System;
using Motel.Models.API.Bases;
using Newtonsoft.Json;

namespace Motel.Models.API.Motels
{
    public class InfoNhaTroResponse:ResponseBase
    {
        [JsonProperty("MaNhaTro")]
        public int MaNhaTro { get; set; }

        [JsonProperty("TenNhaTro")]
        public string TenNhaTro { get; set; }

        [JsonProperty("DiaChi")]
        public string DiaChi { get; set; }

        [JsonProperty("SoPhongTrong")]
        public int SoPhongTrong { get; set; }

        [JsonProperty("SoPhongDangHoatDong")]
        public int SoPhongDangHoatDong { get; set; }

        [JsonProperty("TongSoNguoiDangO")]
        public int TongSoNguoiDangO { get; set; }
    }

   
}
