using Identity.Application.Helpers;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Token.V1.Commands.SignIn
{
    public class SignInV1Command : IRequest<TokenV1Response>
    {
        private string username;
        private string password;

        [JsonProperty("username")]
        public string Username
        {
            get { return username; }
            set { username = value.ToLower(); }
        }

        [JsonProperty("password")]
        public string Password
        {
            get { return password; }
            set { password = CryptHelper.GenerateSHA256String(value); }
        }

        [JsonProperty("customerType")]
        public int CustomerType { get; set; } = 1;

        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("fcmToken")]
        public string FcmToken { get; set; }
    }
}
