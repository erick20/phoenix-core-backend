using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Credential.V1.Queries.GetCredentialById
{
    public class GetCredentialByIdV1Query : IRequest<CredentialV1Response>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
