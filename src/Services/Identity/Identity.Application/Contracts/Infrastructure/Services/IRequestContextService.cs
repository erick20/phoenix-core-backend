using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Infrastructure.Services
{
    public interface IRequestContextService
    {
        string GetAccessToken();

        string GetInnerToken();
    }
}
