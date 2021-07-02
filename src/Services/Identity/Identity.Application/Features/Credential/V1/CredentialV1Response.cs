
using System;

namespace Identity.Application.Features.Credential.V1
{
    public class CredentialV1Response
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Pin { get; set; }
        public DateTime ResetDate { get; set; }
        public short CustomerState { get; set; }
    }
}
