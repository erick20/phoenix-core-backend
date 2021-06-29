using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using entities = Identity.Domain.Entities;

namespace Identity.Application.Features.RoleGroup.V1.Commands.CreateRoleGroup
{
    public class CreateRoleGroupV1CommandHandler : IRequestHandler<CreateRoleGroupV1Command, RoleGroupV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoleGroupV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CreateRoleGroupV1CommandHandler(IMapper mapper, ILogger<CreateRoleGroupV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<RoleGroupV1Response> Handle(CreateRoleGroupV1Command request, CancellationToken cancellationToken)
        {
            entities.RoleGroup entity = await _unitOfWork.RoleGroupRepositoryV1.GetRoleGroupByNameAsync(request.Name , false);
            if (entity is not null)
            {
                ProblemReporter.ReportBadRequest("entity_exists");
            }

            entity = _mapper.Map<entities.RoleGroup>(request);

            _unitOfWork.RoleGroupRepositoryV1.Add(entity);

            _unitOfWork.SaveChanges();

            RoleGroupV1Response response = _mapper.Map<RoleGroupV1Response>(entity);

            return response;
        }
    }
}
