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
using entity = Identity.Domain.Entities;

namespace Identity.Application.Features.RoleGroup.Commands.CreateRoleGroup
{
    public class CreateRoleGroupCommandHandler : IRequestHandler<CreateRoleGroupCommand, RoleGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoleGroupCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CreateRoleGroupCommandHandler(IMapper mapper, ILogger<CreateRoleGroupCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<RoleGroupResponse> Handle(CreateRoleGroupCommand request, CancellationToken cancellationToken)
        {
            entity.RoleGroup entity = await _unitOfWork.RoleGroupRepositoryBase.GetRoleGroupByNameAsync(request.Name , false);
            if (entity is not null)
            {
                ProblemReporter.ReportBadRequest("entity_exists");
            }

            entity = _mapper.Map<entity.RoleGroup>(request);

            _unitOfWork.RoleGroupRepositoryBase.Add(entity);

            _unitOfWork.SaveChanges();

            RoleGroupResponse response = _mapper.Map<RoleGroupResponse>(entity);

            return response;
        }
    }
}
