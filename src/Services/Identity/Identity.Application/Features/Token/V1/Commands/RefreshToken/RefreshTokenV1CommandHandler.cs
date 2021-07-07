using AutoMapper;
using Identity.Application.Contracts.Infrastructure.ClientServices;
using Identity.Application.Contracts.Infrastructure.Services;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Enums;
using Identity.Application.Exceptions;
using Identity.Application.Models.Authentication;
using Identity.Application.Models.UserContext;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using authModels = Identity.Application.Models.Authentication;

namespace Identity.Application.Features.Token.V1.Commands.RefreshToken
{
    public class RefreshTokenV1CommandHandler : IRequestHandler<RefreshTokenV1Command, TokenV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RefreshTokenV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerClientService _customerClientService;
        private readonly IAuthenticationService _authenticationService;
        public RefreshTokenV1CommandHandler(IMapper mapper, ILogger<RefreshTokenV1CommandHandler> logger, IUnitOfWork unitOfWork, ICustomerClientService customerClientService, IAuthenticationService authenticationService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _customerClientService = customerClientService ?? throw new ArgumentNullException(nameof(customerClientService));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }


        public async Task<TokenV1Response> Handle(RefreshTokenV1Command request, CancellationToken cancellationToken)
        {
            TokenV1Response response = default;
            UserContext userContext = _authenticationService.GetContextFromExpiredToken(request.AccessToken);

            authModels.RefreshToken refreshToken = _authenticationService.GetRefreshTokenModel(request.RefreshToken);

            if (userContext.CustomerId != refreshToken.CustomerId)
            {
                ProblemReporter.ReportUnauthorizedAccess("tokens_doesn't_match");
            }
            else if (refreshToken.ExpDate < DateTime.Now)
            {
                ProblemReporter.ReportUnauthorizedAccess("expired_refresh_token");
            }
            else
            {
                var credential = await _unitOfWork.CredentialRepositoryV1.GetCredetialByIdAsync(userContext.CredentialId);
                if (credential.CustomerState == (short)CustomerStateEnum.Blocked || credential.CustomerState == (short)CustomerStateEnum.Deleted)
                {
                    ProblemReporter.ReportAuthenticationFail("blocked_user");
                }

                if (!refreshToken.Magic.Contains(userContext.Magic) ||
                    !credential.Password.Contains(refreshToken.Magic) ||
                    userContext.Version != refreshToken.Version)
                {
                    ProblemReporter.ReportUnauthorizedAccess("tokens_doesn't_match");
                }

                // TODO add column into credetials table "pending updates", for refresh token logic, to get new token claims from database or old token

                TokenDetails tokenDetails = _authenticationService.GetTokenDetails(credential.Id, userContext.CustomerId, (short)userContext.CustomerState,
                                            userContext.DeviceId, credential.Password, userContext.RoleId, userContext.RoleGroupId, userContext.WarehouseId);

                AccessToken accessToken = _authenticationService.CreateAccessToken(tokenDetails);

                response = new()
                {
                    AccessToken = accessToken.Token,
                    ExpDate = accessToken.ExpDate,
                    RefreshToken = _authenticationService.CreateRefreshToken(userContext.CustomerId, DateTime.UtcNow, credential.Password, tokenDetails.Version)
                };

            }

            return response;
        }
    }
}
