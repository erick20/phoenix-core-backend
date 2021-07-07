using AutoMapper;
using Identity.Application.Contracts.Infrastructure.Services;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Enums;
using Identity.Application.Exceptions;
using Identity.Application.Features.Credential.V1;
using Identity.Application.Features.Credential.V1.Queries.GetCredentialById;
using Identity.Application.Helpers;
using Identity.Application.Models.UserContext;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Token.V1.Commands.Authorize
{
    public class AuthorizeV1CommandHandler : IRequestHandler<AuthorizeV1Command, Unit>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorizeV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;
        private readonly IMediator _mediator;
        private readonly IAuthenticationService _authenticationService;
        public AuthorizeV1CommandHandler(IMapper mapper, ILogger<AuthorizeV1CommandHandler> logger, IUnitOfWork unitOfWork, IUserContextService userContextService, IMediator mediator, IAuthenticationService authenticationService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        public async Task<Unit> Handle(AuthorizeV1Command request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.AccessToken))
            {
                ProblemReporter.ReportUnauthorizedAccess("empty_access_token");
            }
            else if (!IsUserAuthenticated())
            {
                // TODO   refactor this logic (same as user context service)
                var Claims = _authenticationService.GetPrincipalFromExpiredToken(request.AccessToken);

                if (Claims == null)
                {
                    ProblemReporter.ReportUnauthorizedAccess("invalid_access_token");
                }

                if (string.IsNullOrEmpty(request.InnerToken))
                {
                    ProblemReporter.ReportUnauthorizedAccess("expired_access_token");
                }
                else
                {
                    UserContext userContextModel = _userContextService.SetUserContext(Claims.Claims);

                    UserContext innerTokenUserContextModel = JsonConvert.DeserializeObject<UserContext>(CryptHelper.Decrypt(request.InnerToken, "aziz")); // TODO secretK

                    if (!userContextModel.Equals(innerTokenUserContextModel))
                    {
                        ProblemReporter.ReportUnauthorizedAccess("invalid_access_token");
                    }
                }
            }
            else
            {
                UserContext userContext = _userContextService.GetUserContext();

                if (userContext == null)
                {
                    ProblemReporter.ReportUnauthorizedAccess("invalid_access_token");
                }

                var query = new GetCredentialByIdV1Query { Id = userContext.CredentialId };
                CredentialV1Response credential = await _mediator.Send(query);

                if (credential != null)
                {
                    if (credential.CustomerState == (short)CustomerStateEnum.Blocked || credential.CustomerState == (short)CustomerStateEnum.Deleted)
                    {
                        ProblemReporter.ReportAuthenticationFail("blocked_user");
                    }

                    if (credential.Password.Substring(0, 5) != userContext.Magic)
                    {
                        ProblemReporter.ReportUnauthorizedAccess("changed_credential");
                    }
                }
                else
                {
                    ProblemReporter.ReportUnauthorizedAccess("changed_credential");
                }
            }
            return Unit.Value;
        }

        private bool IsUserAuthenticated()
        {
            return Context != null && Context.User != null && Context.User.Identity != null && Context.User.Identity.IsAuthenticated;
        }

        private HttpContext Context
        {
            get
            {
                return _contextAccessor.HttpContext;
            }
        }
    }
}
