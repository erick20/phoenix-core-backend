using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            private readonly List<Claim> ClaimRoles;
            //private readonly IHttpContextAccessor _contextAccessor;
            //private readonly IUserContextService _userContextService;
            ////private readonly IInfrastructureService _infrastructureService;
            public AuthorizeFilter(IHttpContextAccessor contextAccessore)
            {
                //_contextAccessor = contextAccessor;
                //_userContextService = userContextService;
                //_infrastructureService = infrastructureService;
            }

            //private HttpContext Context
            //{
            //    get
            //    {
            //        return _contextAccessor.HttpContext;
            //    }
            //}
            //public bool IsUserAuthenticated()
            //{
            //    //return Context != null && Context.User != null && Context.User.Identity != null && Context.User.Identity.IsAuthenticated;
            //}

            public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                var accesstoken = context.HttpContext.Request.Headers["Authorization"].ToString();

                string token = accesstoken.Replace("Bearer ", string.Empty);

                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                if (string.IsNullOrEmpty(accesstoken))
                {
                    //ProblemReporter.ReportUnauthorizedAccess("empty_access_token");
                }
                else if (false)//!IsUserAuthenticated())
                {
                    //var Claims = AuthOptions.GetPrincipalFromExpiredToken(accesstoken); // handle later

                    //if (Claims == null)
                    //{
                    //    ProblemReporter.ReportUnauthorizedAccess("invalid_access_token");
                    //}

                    //var innertoken = context.HttpContext.Request.Headers["InnerAuthorization"].ToString();

                    //if (string.IsNullOrEmpty(innertoken))
                    //{
                    //    ProblemReporter.ReportUnauthorizedAccess("expired_access_token");
                    //}
                    //else
                    //{
                    //    UserContextModel userContextModel = _userContextService.SetUserContext(Claims.Claims);

                    //    UserContextModel innerTokenUserContextModel = JsonConvert.DeserializeObject<UserContextModel>(CryptHelper.Decrypt(innertoken, "aziz")); // TODO secretK

                    //    if (!userContextModel.Equals(innerTokenUserContextModel))
                    //    {
                    //        ProblemReporter.ReportUnauthorizedAccess("invalid_access_token");
                    //    }
                    //}
                }
                else
                {
                    //UserContextModel userContextModel = _userContextService.GetUserContext();

                    //if (userContextModel == null)
                    //{
                    //    ProblemReporter.ReportUnauthorizedAccess("invalid_access_token");
                    //}

                    //CredentialModel credential = await _infrastructureService.GetCredentialAsync(userContextModel.CredentialId);

                    //if (credential != null)
                    //{
                    //    if (credential.CustomerState == CustomerStateEnum.Blocked || credential.CustomerState == CustomerStateEnum.Deleted)
                    //    {
                    //        ProblemReporter.ReportForbiddenError("blocked_user");
                    //    }

                    //    if (credential.Password.Substring(0, 5) != userContextModel.Magic)
                    //    {
                    //        ProblemReporter.ReportUnauthorizedAccess("changed_credential");
                    //    }
                    //}
                    //else
                    //{
                    //    ProblemReporter.ReportUnauthorizedAccess("changed_credential");
                    //}
                }
            }
        }
    }
}
