using System;
using Newtonsoft.Json;

namespace Motel.Models.API.ElectrictyAndWaters
{
    public class ElectrictyAndWaterDto
    {
        [JsonProperty("MaPhong")]
        public int MaPhong { get; set; }

        [JsonProperty("TenPhong")]
        public string TenPhong { get; set; }

        [JsonProperty("NgayThangGhiSo")]
        public DateTime NgayThangGhiSo { get; set; }

        [JsonProperty("ChiSoDienCu")]
        public int ChiSoDienCu { get; set; }

        [JsonProperty("ChiSoDienMoi")]
        public int ChiSoDienMoi { get; set; }

        [JsonProperty("ChiSoNuocCu")]
        public int ChiSoNuocCu { get; set; }

        [JsonProperty("ChiSoNuocMoi")]
        public int ChiSoNuocMoi { get; set; }

        [JsonProperty("DaChotSo")]
        public bool DaChotSo { get; set; }
    }
}
