using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Token.V1.Commands.RefreshToken
{
    public class RefreshTokenV1Command : IRequest<TokenV1Response>
    {
        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("RefreshToken")]
        public string RefreshToken { get; set; }
    }
}
