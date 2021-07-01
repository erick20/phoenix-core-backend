using MediatR;
using Newtonsoft.Json;

namespace Identity.Application.Features.Credential.V1.Commands.UpdateCredentialPassword
{
    public class UpdateCredentialPasswordV1Command : IRequest<Unit>
    {
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonProperty("credentialId")]
        public int CredentialId { get; set; }
                
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
