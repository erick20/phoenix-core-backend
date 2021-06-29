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
using entities = Identity.Domain.Entities;

namespace Identity.Application.Features.PermissionGroup.V1.Commands.UpdatePermissionGroup
{
    public class UpdatePermissionGroupV1CommandHandler : IRequestHandler<UpdatePermissionGroupV1Command, PermissionGroupV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePermissionGroupV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePermissionGroupV1CommandHandler(IMapper mapper, ILogger<UpdatePermissionGroupV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<PermissionGroupV1Response> Handle(UpdatePermissionGroupV1Command request, CancellationToken cancellationToken)
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

            PermissionGroupV1Response response = _mapper.Map<PermissionGroupV1Response>(entity);

            return response;
        }
    }
}
