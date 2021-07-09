using Identity.Application.Models.Authentication;
using Identity.Application.Models.UserContext;
using System;
using System.Security.Claims;

namespace Identity.Application.Contracts.Infrastructure.Services
{
    public interface IAuthenticationService
    {
        TokenDetails GetTokenDetails(int credetialId, int customerId, short customerStateId, int deviceId, string password, int roleId, int roleGroupId, int? warehouseId, int version = default);
        AccessToken CreateAccessToken(TokenDetails details);

        string CreateRefreshToken(int customerId, DateTime expDate, string password, int version);

        RefreshToken GetRefreshTokenModel(string refreshToken);

        UserContext GetContextFromExpiredToken(string accessToken);

        UserContext GetContextFromInnerToken(string innerToken);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
