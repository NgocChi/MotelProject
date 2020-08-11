using Newtonsoft.Json;

namespace Motel.Models.API.Motels
{
    public class NhaTroDto
    {
        [JsonProperty("MaNhaTro")]
        public int MaNhaTro { get; set; }

        [JsonProperty("TenNhaTro")]
        public string TenNhaTro { get; set; }

        [JsonProperty("DiaChi")]
        public string DiaChi { get; set; }

        [JsonProperty("MaChuTro")]
        public int MaChuTro { get; set; }

    }
}
