using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Credential.V1.Commands.UpdateCredetialState
{
    public class UpdateCredentialStateV1CommandHandler : IRequestHandler<UpdateCredentialStateV1Command>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCredentialStateV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCredentialStateV1CommandHandler(IMapper mapper, ILogger<UpdateCredentialStateV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Unit> Handle(UpdateCredentialStateV1Command request, CancellationToken cancellationToken)
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
