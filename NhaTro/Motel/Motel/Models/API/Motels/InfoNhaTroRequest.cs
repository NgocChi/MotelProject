using Motel.Models.API.Bases;
using Newtonsoft.Json;

namespace Motel.Models.API.Motels
{
    public class InfoNhaTroRequest : RequestBase
    {
        [JsonProperty("MaNhaTro")]
        public int MaNhaTro { get; set; }
    }
}
