using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using Identity.Application.Features.RolesGroup.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using entities = Identity.Domain.Entities;

namespace Identity.Application.Features.RoleGroup.Commands.UpdateRoleGroup
{
    public class UpdateRoleGroupCommandHandler : IRequestHandler<UpdateRoleGroupCommand, RoleGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoleGroupCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateRoleGroupCommandHandler(IMapper mapper, ILogger<UpdateRoleGroupCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<RoleGroupResponse> Handle(UpdateRoleGroupCommand request, CancellationToken cancellationToken)
        {
            var existsEntity = await _unitOfWork.RoleGroupRepositoryBase.GetRoleGroupByNameAsync(request.Name, false);
            if (existsEntity is not null)
            {
                ProblemReporter.ReportBadRequest("entity_exists");
            }

            entities.RoleGroup entity = await _unitOfWork.RoleGroupRepositoryBase.GetRoleGroupByIdAsync(request.Id, false);

            if (entity is null)
            {
                ProblemReporter.ReportResourseNotfound("entity_not_found");
            }

            entity = _mapper.Map<entities.RoleGroup>(entity);

            _unitOfWork.RoleGroupRepositoryBase.Add(entity);

            _unitOfWork.SaveChanges();

            RoleGroupResponse response = _mapper.Map<RoleGroupResponse>(entity);

            return response;
        }
    }
}
