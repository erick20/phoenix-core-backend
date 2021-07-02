using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Token.V1
{
    public class TokenV1Response
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("expDate")]
        public long ExpDate { get; set; }
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
