using System;
using Motel.Models.API.Bases;
using Newtonsoft.Json;

namespace Motel.Models.API.Contacts
{
    public class HopDongInfoResponse: ResponseBase
    {
        [JsonProperty("TenNhaTro")]
        public string TenNhaTro { get; set; }

        [JsonProperty("DiaChi")]
        public string DiaChi { get; set; }

        [JsonProperty("TenPhong")]
        public string TenPhong { get; set; }

        [JsonProperty("NgayBatDau")]
        public DateTime? NgayBatDau { get; set; }

        [JsonProperty("NgayKetThuc")]
        public DateTime? NgayKetThuc { get; set; }
    }

    public class HopDongInfoRequest : RequestBase
    {
        [JsonProperty("MaHopDong")]
        public int MaHopDong { get; set; }
    }
}
