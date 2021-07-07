using Identity.Application.Features.Token.V1.Commands.Authorize;
using MediatR;
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
            private readonly IMediator _mediator;
            public AuthorizeFilter(IMediator mediator)
            {
                _mediator = mediator;
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

                AuthorizeV1Command command = new()
                {
                    AccessToken = accessToken,
                    InnerToken = innerToken
                };
                await _mediator.Send(command);
            }
        }
    }
}
