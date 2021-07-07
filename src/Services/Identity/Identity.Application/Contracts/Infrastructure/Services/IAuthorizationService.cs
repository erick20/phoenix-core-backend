﻿using Identity.Application.Models.UserContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Infrastructure.Services
{
    public interface IAuthorizationService
    {
         Task Authorize(string accessToken, string innerToken);

         UserContext GetContextFromExpiredToken(string accessToken);
    }
}
