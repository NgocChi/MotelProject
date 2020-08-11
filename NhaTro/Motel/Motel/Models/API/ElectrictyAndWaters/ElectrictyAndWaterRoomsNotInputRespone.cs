using System.Collections.Generic;
using Motel.Models.API.Bases;
using Motel.Models.API.Rooms;
using Newtonsoft.Json;

namespace Motel.Models.API.ElectrictyAndWaters
{
    public class ElectrictyAndWaterRoomsNotInputRespone : ResponseBase
    {
        [JsonProperty("PhongDtos")]
        public IList<PhongDto> PhongDtos { get; set; }
    }
}
