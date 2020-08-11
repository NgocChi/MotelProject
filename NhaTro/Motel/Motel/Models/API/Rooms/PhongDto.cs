using Newtonsoft.Json;

namespace Motel.Models.API.Rooms
{
    public class PhongDto
    {
        [JsonProperty("MaPhong")]
        public int MaPhong { get; set; }

        [JsonProperty("TenPhong")]
        public string TenPhong { get; set; }

        [JsonProperty("MaNhaTro")]
        public int MaNhaTro { get; set; }

        [JsonProperty("TenNhaTro")]
        public string TenNhaTro { get; set; }
    }
}
