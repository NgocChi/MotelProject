using Motel.Models.API.Bases;
using Newtonsoft.Json;

namespace Motel.Models.API.ElectrictyAndWaters
{
    public class ElectrictyAndWaterRequest : RequestBase
    {
        [JsonProperty("YearTimeInput")]
        public int YearTimeInput { get; set; }

        [JsonProperty("DayTimeInput")]
        public int DayTimeInput { get; set; }

        [JsonProperty("MaNhaTro")]
        public int MaNhaTro { get; set; }
    }
}
