using Identity.Application.Contracts.Infrastructure.Services;
using Identity.Application.Enums;
using Identity.Application.Exceptions;
using Identity.Application.Features.Credential.V1;
using Identity.Application.Features.Credential.V1.Queries.GetCredentialById;
using Identity.Application.Helpers;
using Identity.Application.Models.UserContext;
using Identity.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {        
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserContextService _userContextService;
        private readonly IMediator _mediator;
        private readonly ILogger<AuthorizationService> _logger;
        private readonly IOptions<AuthenticationSettings> _authenticationServiceSettings;
        public AuthorizationService(IHttpContextAccessor contextAccessor, IUserContextService userContextService, IMediator mediator, IOptions<AuthenticationSettings> authenticationServiceSettings, ILogger<AuthorizationService> logger)
        {
            _contextAccessor = contextAccessor;
            _userContextService = userContextService;
            _mediator = mediator;
            _authenticationServiceSettings = authenticationServiceSettings;
            _logger = logger;
        }
        public async Task Authorize(string accessToken, string innerToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                ProblemReporter.ReportUnauthorizedAccess("empty_access_token");
            }
            else if (!IsUserAuthenticated())
            {
                var Claims = GetPrincipalFromExpiredToken(accessToken);

                if (Claims == null)
                {
                    ProblemReporter.ReportUnauthorizedAccess("invalid_access_token");
                }

                if (string.IsNullOrEmpty(innerToken))
                {
                    ProblemReporter.ReportUnauthorizedAccess("expired_access_token");
                }
                else
                {
                    UserContext userContextModel = _userContextService.SetUserContext(Claims.Claims);

                    UserContext innerTokenUserContextModel = JsonConvert.DeserializeObject<UserContext>(CryptHelper.Decrypt(innerToken, "aziz")); // TODO secretK

                    if (!userContextModel.Equals(innerTokenUserContextModel))
                    {
                        ProblemReporter.ReportUnauthorizedAccess("invalid_access_token");
                    }
                }
            }
            else
            {
                UserContext userContext = _userContextService.GetUserContext();

                if (userContext == null)
                {
                    ProblemReporter.ReportUnauthorizedAccess("invalid_access_token");
                }

                var query = new GetCredentialByIdV1Query { Id = userContext.CredentialId };
                CredentialV1Response credential = await _mediator.Send(query);

                if (credential != null)
                {
                    if (credential.CustomerState == (short)CustomerStateEnum.Blocked || credential.CustomerState == (short)CustomerStateEnum.Deleted)
                    {
                        ProblemReporter.ReportAuthenticationFail("blocked_user");
                    }

                    if (credential.Password.Substring(0, 5) != userContext.Magic)
                    {
                        ProblemReporter.ReportUnauthorizedAccess("changed_credential");
                    }
                }
                else
                {
                    ProblemReporter.ReportUnauthorizedAccess("changed_credential");
                }
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            token = token.Replace("Bearer ", string.Empty);
            ClaimsPrincipal principal = null;

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _authenticationServiceSettings.Value.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _authenticationServiceSettings.Value.Audience,
                    ValidateLifetime = false,
                    IssuerSigningKey = CryptHelper.GetSymmetricSecurityKey(_authenticationServiceSettings.Value.Key),
                    ValidateIssuerSigningKey = true,
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    ErrorModel errorModel = new()
                    {
                        Key = "invalid_token",
                    };

                    // change this exception handling
                    //ProblemReporter.ReportUnauthorizedAccess(JsonConvert.SerializeObject(errorModel));// logout
                }
            }
            catch (Exception ex)
            {
                ErrorModel errorModel = new()
                {
                    Key = "invalid_token",
                };

                // change this exception handling

                ProblemReporter.ReportUnauthorizedAccess(JsonConvert.SerializeObject(errorModel));
            }
            return principal;
        }
                
        private bool IsUserAuthenticated()
        {
            return Context != null && Context.User != null && Context.User.Identity != null && Context.User.Identity.IsAuthenticated;
        }

        private HttpContext Context
        {
            get
            {
                return _contextAccessor.HttpContext;
            }
        }




    }
}
