using Identity.Application.Enums;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.API.Settings
{
    //public class AuthenticationSettings
    //{
    //    public const string ISSUER = "HayPost";
    //    public const string AUDIENCE = "https://localhost:44332/";
    //    const string KEY = "3B0b74dbbc6HK022argregvfaeFVE";
    //    public const int LIFETIME = 8;
    //    public const int ADMINLIFETIME = 250;
    //    public static SymmetricSecurityKey GetSymmetricSecurityKey(string key = KEY)
    //    {
    //        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    //    }

    //    private static ClaimsIdentity GetIdentity(int credetialId, int customerId, int deviceId, string email, int role, int roleGroupId, string magic, CustomerStateEnum customerStateClaim, int? warehouseId)
    //    {
    //        var claims = new List<Claim>
    //                                    {
    //                                     new Claim("CredentialId", credetialId.ToString()),
    //                                     new Claim("CustomerId", customerId.ToString()),
    //                                     new Claim("DeviceId", deviceId.ToString()),
    //                                     new Claim("Email", email),
    //                                     new Claim("RoleId", role.ToString()),
    //                                     new Claim("RoleGroupId", roleGroupId.ToString()),
    //                                     new Claim("State", customerStateClaim.ToString()),
    //                                     new Claim("Magic", magic),
    //                                     new Claim("WarehouseId", warehouseId.ToString())
    //                                    };

    //        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");

    //        return claimsIdentity;
    //    }

    //    public static ClaimsIdentity GetIdentity(params KeyValuePair<string, object>[] pairs)
    //    {
    //        var claims = new List<Claim>();
    //        if (pairs.Any())
    //        {
    //            foreach (var parameter in pairs)
    //            {
    //                claims.Add(new Claim(parameter.Key, parameter.Value.ToString()));
    //            }
    //        }

    //        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

    //        return claimsIdentity;
    //    }

    //    public static AccessTokenExpDateModel Authenticate(int credentialId, int customerId, int deviceId, string email, int roleId, int roleGroupId, string magic, CustomerStateEnum customerStateClaim, int? warehouseId)
    //    {
    //        AccessTokenExpDateModel result = null;
    //        ClaimsIdentity _identity = GetIdentity(credentialId, customerId, deviceId, email, roleId, roleGroupId, magic, customerStateClaim, warehouseId);
    //        DateTime currentDate = DateTime.Now;
    //        DateTime expDateTime = currentDate.Add(TimeSpan.FromHours(LIFETIME));
    //        //DateTime expDateTime = currentDate.Add(TimeSpan.FromDays(365));
    //        string _encodedJwt = string.Empty;

    //        var _jwt = new JwtSecurityToken(
    //                        issuer: AuthOptions.ISSUER,
    //                        audience: AuthOptions.AUDIENCE,
    //                        notBefore: currentDate,
    //                        claims: _identity.Claims,
    //                        expires: expDateTime,
    //                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(KEY), SecurityAlgorithms.HmacSha256));

    //        _encodedJwt = new JwtSecurityTokenHandler().WriteToken(_jwt);

    //        result = new AccessTokenExpDateModel
    //        {
    //            AccessToken = _encodedJwt,
    //            ExpDate = expDateTime
    //        };

    //        return result;
    //    }

    //    public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    //    {
    //        token = token.Replace("Bearer ", string.Empty);
    //        ClaimsPrincipal principal = null;

    //        try
    //        {
    //            var tokenValidationParameters = new TokenValidationParameters
    //            {
    //                ValidateIssuer = true,
    //                ValidIssuer = AuthOptions.ISSUER,
    //                ValidateAudience = true,
    //                ValidAudience = AuthOptions.AUDIENCE,
    //                ValidateLifetime = false,
    //                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(KEY),
    //                ValidateIssuerSigningKey = true,
    //            };

    //            var tokenHandler = new JwtSecurityTokenHandler();
    //            SecurityToken securityToken;
    //            principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
    //            var jwtSecurityToken = securityToken as JwtSecurityToken;
    //            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
    //            {
    //                AuthorizationErrorModel errorModel = new AuthorizationErrorModel()
    //                {
    //                    Key = "invalid_token",
    //                };
    //                ProblemReporter.ReportUnauthorizedAccess(JsonConvert.SerializeObject(errorModel));// logout
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            AuthorizationErrorModel errorModel = new AuthorizationErrorModel
    //            {
    //                Key = "invalid_access_token"
    //            };

    //            ProblemReporter.ReportUnauthorizedAccess(JsonConvert.SerializeObject(errorModel));
    //        }
    //        return principal;
    //    }
    //}
}
