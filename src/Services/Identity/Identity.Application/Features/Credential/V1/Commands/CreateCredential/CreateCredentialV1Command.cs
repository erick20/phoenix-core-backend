using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Credential.V1.Commands.CreateCredential
{
    public class CreateCredentialV1Command : IRequest<Unit>
    {
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("customerStateId")]
        public short CustomerStateId { get; set; }
    }
}
