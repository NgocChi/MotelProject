using System;
using Motel.Models.API.Bases;
using Newtonsoft.Json;

namespace Motel.Models.API.Motels
{
    public class ListNhaTroRequest:RequestBase
    {
        [JsonProperty("IdUser")]
        public int IdUser { get; set; }
    }
}
