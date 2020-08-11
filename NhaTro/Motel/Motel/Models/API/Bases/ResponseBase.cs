using System;
using Newtonsoft.Json;

namespace Motel.Models.API.Bases
{
    public class RequestBase
    {
        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }
    }

    public class ResponseBase
    {
        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("StatusCode")]
        public int StatusCode { get; set; }
    }
}
