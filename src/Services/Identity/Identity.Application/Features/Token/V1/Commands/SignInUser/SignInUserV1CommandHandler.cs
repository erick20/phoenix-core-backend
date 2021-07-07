﻿using AutoMapper;
using Identity.Application.Contracts.Infrastructure.ClientServices;
using Identity.Application.Contracts.Infrastructure.Services;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Enums;
using Identity.Application.Exceptions;
using Identity.Application.Models.Authentication;
using Identity.Application.Models.CustomerClient;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Token.V1.Commands.SignInUser
{
    public class SignInUserV1CommandHandler : IRequestHandler<SignInUserV1Command, TokenV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SignInUserV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerClientService _customerClientService;
        private readonly IAuthenticationService _authenticationService;
        public SignInUserV1CommandHandler(IMapper mapper, ILogger<SignInUserV1CommandHandler> logger, IUnitOfWork unitOfWork, ICustomerClientService customerClientService, IAuthenticationService authenticationService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _customerClientService = customerClientService ?? throw new ArgumentNullException(nameof(customerClientService));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }
        public async Task<TokenV1Response> Handle(SignInUserV1Command request, CancellationToken cancellationToken)
        {
            GetCustomerClientResponse customer = await _customerClientService.GetCustomerByContactAsync(request.Username);
            if (customer is null || (int)customer.Customer.CustomerType != request.CustomerType)
            {
                ProblemReporter.ReportAuthenticationFail("incorrect_username_or_password");
            }

            if (customer.Customer.CustomerState != CustomerStateEnum.Activated)
            {
                ProblemReporter.ReportAuthenticationFail("blocked_or_not_activated");
            }

            DeviceClientResponse deviceClient = customer.Devices?.Where(x => x.DeviceId == request.DeviceId && x.FsmToken == request.FcmToken)?.FirstOrDefault();

            if (deviceClient is null)
            {
                // deviceClient = add or update device
            }

            AccessToken accessToken = _authenticationService.CreateAccessToken();

            TokenV1Response response = default;

            return response;
        }
    }
}
