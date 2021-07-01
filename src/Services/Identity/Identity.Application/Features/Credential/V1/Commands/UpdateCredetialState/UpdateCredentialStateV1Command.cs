using Identity.Application.Enums;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Credential.V1.Commands.UpdateCredetialState
{
    public class UpdateCredentialStateV1Command : IRequest<Unit>
    {
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonProperty("credentialId")]
        public int CredentialId { get; set; }

        [JsonProperty("customerState")]
        public CustomerStateEnum CustomerState { get; set; }
    }
}
