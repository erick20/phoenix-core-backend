using Identity.Application.Contracts.Infrastructure;
using Identity.Application.Enums;
using Identity.Application.Models.UserContext;
using Identity.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Identity.Infrastructure.Services
{
    public class UserContextService : IUserContextService
    {
        private UserContext userContext;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserContextService(IHttpContextAccessor contextAccessor)
        {

            _contextAccessor = contextAccessor;
        }

        private HttpContext Context
        {
            get
            {
                return _contextAccessor.HttpContext;
            }
        }
        public UserContext GetUserContext()
        {
            if (userContext == null)
            {
                var claims = Context?.User?.Claims;

                userContext = SetUserContext(claims);
            }

            return userContext;
        }

        public UserContext SetUserContext(IEnumerable<Claim> claims)
        {
            int.TryParse(claims.First(p => p.Type == "WarehouseId").Value, out int warehouseId);
            this.userContext = new UserContext
            {
                DeviceId = int.Parse(claims.First(p => p.Type == "DeviceId").Value),
                CustomerId = int.Parse(claims.First(p => p.Type == "CustomerId").Value),
                Email = claims.First(p => p.Type == "Email").Value,
                RoleId = int.Parse(claims.First(p => p.Type == "RoleId").Value),
                RoleGroupId = int.Parse(claims.First(p => p.Type == "RoleGroupId").Value),
                Magic = claims.First(p => p.Type == "Magic").Value,
                CustomerState = claims.First(p => p.Type == "State").Value.ToEnum<CustomerStateEnum>(),
                CredentialId = int.Parse(claims.First(p => p.Type == "CredentialId").Value),
                ExpDate = long.Parse(claims.First(p => p.Type == "exp").Value),
                WarehouseId = warehouseId,
            };

            return this.userContext;
        }
    }
}
