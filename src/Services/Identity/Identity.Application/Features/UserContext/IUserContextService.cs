using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.UserContext
{
    public interface IUserContextService
    {
        UserContext GetUserContext();
        void SetUserContext(UserContext userContext);
        UserContext SetUserContext(string accessToken);
    }
}
