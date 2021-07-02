using Identity.Application.Contracts.Infrastructure;
using Identity.Application.Exceptions;
using Identity.Application.Models.Authentication;
using Identity.Infrastructure.Helpers;
using Identity.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IOptions<AuthenticationServiceSettings> _authenticationServiceSettings;
        public AuthenticationService(ILogger<AuthenticationService> logger, IOptions<AuthenticationServiceSettings> authenticationServiceSettings)
        {
            _logger = logger;
            _authenticationServiceSettings = authenticationServiceSettings;
        }

        public AccessToken CreateAccessToken(TokenDetails details)
        {
            ClaimsIdentity _identity = GetIdentity(details);
            DateTime currentDate = DateTime.Now;
            DateTime expDateTime = currentDate.Add(TimeSpan.FromHours(_authenticationServiceSettings.Value.CustomerTokenLifeTime));
            string _encodedJwt = string.Empty;

            var _jwt = new JwtSecurityToken(
                            issuer: _authenticationServiceSettings.Value.Issuer,
                            audience: _authenticationServiceSettings.Value.Audience,
                            notBefore: currentDate,
                            claims: _identity.Claims,
                            expires: expDateTime,
                            signingCredentials: new SigningCredentials(CryptHelper.GetSymmetricSecurityKey(_authenticationServiceSettings.Value.Key), SecurityAlgorithms.HmacSha256));

            _encodedJwt = new JwtSecurityTokenHandler().WriteToken(_jwt);

            AccessToken result = new()
            {
                Token = _encodedJwt,
                ExpDate = expDateTime
            };

            return result;
        }

        private static ClaimsIdentity GetIdentity(TokenDetails details)
        {
            var claims = new List<Claim>
            {
                new Claim("CredentialId", details.CredentialId.ToString()),
                new Claim("CustomerId", details.CustomerId.ToString()),
                new Claim("DeviceId", details.DeviceId.ToString()),
                new Claim("Email", details.Email),
                new Claim("RoleId", details.RoleId.ToString()),
                new Claim("RoleGroupId", details.RoleGroupId.ToString()),
                new Claim("State", details.CustomerState.ToString()),
                new Claim("Magic", details.Magic),
                new Claim("WarehouseId", details.WarehouseId.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");

            return claimsIdentity;
        }
    }
}
