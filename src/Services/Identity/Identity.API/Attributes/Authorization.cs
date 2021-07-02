using Identity.Application.Contracts.Infrastructure.Services;
using Identity.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Identity.API.Attributes
{
    public class AuthorizationAttribute : TypeFilterAttribute
    {
        public AuthorizationAttribute() : base(typeof(AuthorizeFilter))
        {
        }

        public class AuthorizeFilter : IAsyncAuthorizationFilter
        {
            private readonly IAuthorizationService _authorizationService;
            public AuthorizeFilter(IAuthorizationService authorizationService)
            {
                _authorizationService = authorizationService;
            }

            public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                var accessToken = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var innerToken = context.HttpContext.Request.Headers["InnerAuthorization"].ToString();

                if (context == null)
                {
                    // Log Here
                    throw new ArgumentNullException(nameof(context));
                }

                await _authorizationService.Authorize(accessToken, innerToken);
            }
        }
    }
}
