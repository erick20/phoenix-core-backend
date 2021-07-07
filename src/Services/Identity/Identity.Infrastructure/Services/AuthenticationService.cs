using Identity.Application.Contracts.Infrastructure.Services;
using Identity.Application.Exceptions;
using Identity.Application.Helpers;
using Identity.Application.Models.Authentication;
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
        private readonly IOptions<AuthenticationSettings> _authenticationServiceSettings;
        public AuthenticationService(ILogger<AuthenticationService> logger, IOptions<AuthenticationSettings> authenticationServiceSettings)
        {
            _logger = logger;
            _authenticationServiceSettings = authenticationServiceSettings;
        }

        public TokenDetails GetTokenDetails(int credetialId, int customerId, short customerStateId, int deviceId, string password, int roleId, int roleGroupId, int? warehouseId = null, int version = default)
        {
            return new TokenDetails
            {
                CredentialId = credetialId,
                CustomerId = customerId,
                CustomerStateId = customerStateId,
                DeviceId = deviceId,
                Magic = password.Substring(0, 5),
                RoleId = roleId,
                RoleGroupId = roleGroupId,
                WarehouseId = warehouseId,
                Version = version++
            };
        }

        public string CreateRefreshToken(int customerId, DateTime expDate, string password, int version)
        {
            RefreshToken refreshTokenModel = new()
            {
                CustomerId = customerId,
                ExpDate = expDate.AddMonths(5),
                Magic = password.Substring(0, 5)
            };

            string refreshToken = CryptHelper.Encrypt(JsonConvert.SerializeObject(refreshTokenModel), _authenticationServiceSettings.Value.AesSecretKey);

            return refreshToken;
        }

        public RefreshToken GetRefreshTokenModel(string refreshToken)
        {
            RefreshToken refreshTokenModel = JsonConvert.DeserializeObject<RefreshToken>(CryptHelper.Decrypt(refreshToken, _authenticationServiceSettings.Value.AesSecretKey));

            return refreshTokenModel;
        }

        public AccessToken CreateAccessToken(TokenDetails details)
        {
            ClaimsIdentity _identity = GetIdentity(details);
            DateTime currentDate = DateTime.UtcNow;
            DateTime expDateTime = currentDate.Add(TimeSpan.FromHours(_authenticationServiceSettings.Value.TokenLifeTime));
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
                Token = "Bearer " + _encodedJwt,
                ExpDate = DatetimeHelper.DatetimeToUnixParser(expDateTime) 
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
                new Claim("RoleId", details.RoleId.ToString()),
                new Claim("RoleGroupId", details.RoleGroupId.ToString()),
                new Claim("StateId", details.CustomerStateId.ToString()),
                new Claim("Magic", details.Magic),
                new Claim("WarehouseId", details.WarehouseId.ToString()),
                new Claim("version", details.Version.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");

            return claimsIdentity;
        }
    }
}
