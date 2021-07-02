using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Token.V1.Commands.SignIn
{
    public class SignInV1CommandHandler : IRequestHandler<SignInV1Command, TokenV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SignInV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public SignInV1CommandHandler(IMapper mapper, ILogger<SignInV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<TokenV1Response> Handle(SignInV1Command request, CancellationToken cancellationToken)
        {
            // Get Customer From Client Service

            //

            TokenV1Response response = default;

            return response;
        }
    }
}
