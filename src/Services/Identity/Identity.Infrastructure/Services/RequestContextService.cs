using Identity.Application.Contracts.Infrastructure.Services;
using Identity.Application.Helpers;
using Identity.Application.Models.UserContext;
using Identity.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq;

namespace Identity.Infrastructure.Services
{
    internal class RequestContextService : IRequestContextService
    {
        private string accessToken;
        private string innerToken;

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserContextService _userContextService;
        private readonly SecretKeysClientSettings _secretKeysClientSettings;
        public RequestContextService(IHttpContextAccessor contextAccessor, IUserContextService userContextService, IOptions<SecretKeysClientSettings> secretKeysClientSettings)
        {
            _contextAccessor = contextAccessor;
            _userContextService = userContextService;
            _secretKeysClientSettings = secretKeysClientSettings.Value;
        }

        private HttpContext Context => _contextAccessor.HttpContext;

        public string GetAccessToken()
        {
            accessToken = Context?.Request?.Headers?.FirstOrDefault(x => x.Key.ToLower() == "authorization").Value ?? "";

            accessToken = accessToken.Replace("Bearer ", "");

            return accessToken;
        }

        public string GetInnerToken()
        {
            UserContext userContext = _userContextService.GetUserContext();
            if (userContext is null)
            {
                userContext = new UserContext
                {
                    Magic = "micro"
                };
            }

            return innerToken = CryptHelper.Encrypt(JsonConvert.SerializeObject(userContext), _secretKeysClientSettings.AesSecretKey);
        }

    }
}
