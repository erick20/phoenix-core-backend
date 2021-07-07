using Identity.Application.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Infrastructure.Services
{
    public interface IAuthenticationService
    {
        TokenDetails GetTokenDetails(int credetialId, int customerId, short customerStateId, int deviceId, string password, int roleId, int roleGroupId, int? warehouseId, int version = default);
        AccessToken CreateAccessToken(TokenDetails details);

        string CreateRefreshToken(int customerId, DateTime expDate, string password, int version);

        RefreshToken GetRefreshTokenModel(string refreshToken);
    }
}
