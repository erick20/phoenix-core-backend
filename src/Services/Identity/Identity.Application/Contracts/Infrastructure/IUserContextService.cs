using Identity.Application.Models.UserContext;
using System.Collections.Generic;
using System.Security.Claims;

namespace Identity.Application.Contracts.Infrastructure
{
    public interface IUserContextService
    {
        UserContext GetUserContext();
        UserContext SetUserContext(IEnumerable<Claim> claims);
    }
}
