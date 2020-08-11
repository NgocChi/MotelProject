using System.Collections.Generic;
using Motel.Models.API.Bases;
using Newtonsoft.Json;

namespace Motel.Models.API.ElectrictyAndWaters
{
    public class ElectrictyAndWaterResponse : ResponseBase
    {
        [JsonProperty("MaPhong")]
        public int MaPhong { get; set; }

        [JsonProperty("DaChotSo")]
        public bool DaChotSo { get; set; }

        [JsonProperty("ElectrictyAndWaterDtos")]
        public IList<ElectrictyAndWaterDto> ElectrictyAndWaterDtos { get; set; }

    }
}
