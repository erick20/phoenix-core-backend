using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using entities = Identity.Domain.Entities;

namespace Identity.Application.Features.PermissionGroup.V1.Commands.CreatePermissionGroup
{
    public class CreatePermissionGroupV1CommandHandler : IRequestHandler<CreatePermissionGroupV1Command, PermissionGroupV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePermissionGroupV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePermissionGroupV1CommandHandler(IMapper mapper, ILogger<CreatePermissionGroupV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<PermissionGroupV1Response> Handle(CreatePermissionGroupV1Command request, CancellationToken cancellationToken)
        {
            entities.PermissionGroup entity = await _unitOfWork.PermissionGroupRepositoryBase.GetPermissionGroupByNameAsync(request.Name, false);
            if (entity is not null)
            {
                ProblemReporter.ReportBadRequest("entity_exists");
            }

            entity = _mapper.Map<entities.PermissionGroup>(request);

            _unitOfWork.PermissionGroupRepositoryV1.Add(entity);

            _unitOfWork.SaveChanges();

            PermissionGroupV1Response response = _mapper.Map<PermissionGroupV1Response>(entity);

            return response;
        }
    }
}
