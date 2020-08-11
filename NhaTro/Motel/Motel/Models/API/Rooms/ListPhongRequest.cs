using Motel.Models.API.Bases;
using Newtonsoft.Json;

namespace Motel.Models.API.Rooms
{
    public class ListPhongRequest : RequestBase
    {
        [JsonProperty("MaKhachThue")]
        public int MaKhachThue { get; set; }

        [JsonProperty("MaNhaTro")]
        public int MaNhaTro { get; set; }
    }
}
