using System;
using System.Collections.Generic;
using Motel.Models.API.Bases;
using Newtonsoft.Json;

namespace Motel.Models.API.Rooms
{
    public class ListPhongResponse : ResponseBase
    {
        [JsonProperty("PhongDtos")]
        public IList<PhongDto> PhongDtos { get; set; }
    }
}
