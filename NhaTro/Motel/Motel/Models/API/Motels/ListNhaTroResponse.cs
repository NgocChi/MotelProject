using System.Collections.Generic;
using Motel.Models.API.Bases;
using Newtonsoft.Json;

namespace Motel.Models.API.Motels
{
    public class ListNhaTroResponse : ResponseBase
    {
        [JsonProperty("NhaTroDtos")]
        public IList<NhaTroDto> NhaTroDtos { get; set; }
    }
}
