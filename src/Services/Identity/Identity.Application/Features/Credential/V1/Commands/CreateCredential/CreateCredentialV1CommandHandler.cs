using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using entities = Identity.Domain.Entities;

namespace Identity.Application.Features.Credential.V1.Commands.CreateCredential
{
    public class CreateCredentialV1CommandHandler : IRequestHandler<CreateCredentialV1Command>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCredentialV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCredentialV1CommandHandler(IMapper mapper, ILogger<CreateCredentialV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Unit> Handle(CreateCredentialV1Command request, CancellationToken cancellationToken)
        {
            entities.Credential credential = _mapper.Map<entities.Credential>(request);

            _unitOfWork.CredentialRepositoryV1.Add(credential);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
