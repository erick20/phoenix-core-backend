using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using entities = Identity.Domain.Entities;

namespace Identity.Application.Features.RoleGroup.V1.Commands.UpdateRoleGroup
{
    public class UpdateRoleGroupV1CommandHandler : IRequestHandler<UpdateRoleGroupV1Command, RoleGroupV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoleGroupV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateRoleGroupV1CommandHandler(IMapper mapper, ILogger<UpdateRoleGroupV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<RoleGroupV1Response> Handle(UpdateRoleGroupV1Command request, CancellationToken cancellationToken)
        {
            var existsEntity = await _unitOfWork.RoleGroupRepositoryV1.GetRoleGroupByNameAsync(request.Name, false);
            if (existsEntity is not null)
            {
                ProblemReporter.ReportBadRequest("entity_exists");
            }

            entities.RoleGroup entity = await _unitOfWork.RoleGroupRepositoryV1.GetRoleGroupByIdAsync(request.Id, false);

            if (entity is null)
            {
                ProblemReporter.ReportResourseNotfound("entity_not_found");
            }

            entity = _mapper.Map<entities.RoleGroup>(entity);

            _unitOfWork.RoleGroupRepositoryBase.Add(entity);

            _unitOfWork.SaveChanges();

            RoleGroupV1Response response = _mapper.Map<RoleGroupV1Response>(entity);

            return response;
        }
    }
}
