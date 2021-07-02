using Identity.Application.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Infrastructure
{
    public interface IAuthenticationService
    {
        AccessToken CreateAccessToken(TokenDetails details);
    }
}
