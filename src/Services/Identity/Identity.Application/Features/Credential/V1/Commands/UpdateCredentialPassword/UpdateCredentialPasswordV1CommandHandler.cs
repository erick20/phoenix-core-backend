using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using entites = Identity.Domain.Entities;

namespace Identity.Application.Features.Credential.V1.Commands.UpdateCredentialPassword
{
    public class UpdateCredentialPasswordV1CommandHandler : IRequestHandler<UpdateCredentialPasswordV1Command>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCredentialPasswordV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCredentialPasswordV1CommandHandler(IMapper mapper, ILogger<UpdateCredentialPasswordV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Unit> Handle(UpdateCredentialPasswordV1Command request, CancellationToken cancellationToken)
        {
            var credetial = await _unitOfWork.CredentialRepositoryV1.GetCredetialByIdAsync(request.CredentialId, true);

            if (credetial is null)
            {
                ProblemReporter.ReportResourseNotfound("credential_not_found");
            }

            credetial = _mapper.Map(request, credetial);

            _unitOfWork.CredentialRepositoryV1.Update(credetial);

            _unitOfWork.SaveChanges();

            return Unit.Value;
        }
    }
}
