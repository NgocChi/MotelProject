using System;
using System.Collections.Generic;
using Motel.Models.API.Bases;
using Newtonsoft.Json;

namespace Motel.Models.API.ElectrictyAndWaters
{
    public class ElectrictyAndWaterRequest : RequestBase
    {
        [JsonProperty("TimeInput")]
        public DateTime TimeInput { get; set; }

        [JsonProperty("MaNhaTro")]
        public int MaNhaTro { get; set; }
    }

    public class ElectrictyAndWaterResponse : ResponseBase
    {
        [JsonProperty("MaPhong")]
        public int MaPhong { get; set; }

        [JsonProperty("DaChotSo")]
        public bool DaChotSo { get; set; }

        [JsonProperty("ElectrictyAndWaterDtos")]
        public IList<ElectrictyAndWaterDto> ElectrictyAndWaterDtos { get; set; }

    }

    public class ElectrictyAndWaterDto
    {
        [JsonProperty("MaPhong")]
        public int MaPhong { get; set; }

        [JsonProperty("TenPhong")]
        public string TenPhong { get; set; }

        [JsonProperty("ChiSoDienCu")]
        public int ChiSoDienCu { get; set; }

        [JsonProperty("ChiSoDienMoi")]
        public int ChiSoDienMoi { get; set; }

        [JsonProperty("ChiSoNuocCu")]
        public int ChiSoNuocCu { get; set; }

        [JsonProperty("ChiSoNuocMoi")]
        public int ChiSoNuocMoi { get; set; }
    }
}
