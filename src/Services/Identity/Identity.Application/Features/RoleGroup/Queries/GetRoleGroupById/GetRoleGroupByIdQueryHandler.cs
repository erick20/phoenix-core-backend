using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using entity = Identity.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.RolesGroup.Queries.GetRolesGroupById
{
    public class GetRoleGroupByIdQueryHandler : IRequestHandler<GetRoleGroupByIdQuery, RoleGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleGroupByIdQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetRoleGroupByIdQueryHandler(IMapper mapper, ILogger<GetRoleGroupByIdQueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<RoleGroupResponse> Handle(GetRoleGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.RoleGroupRepositoryBase.GetRoleGroupByIdAsync(request.Id, false);

            if (entity is null)
            {
                ProblemReporter.ReportResourseNotfound("role_group_not_found");
            }

            RoleGroupResponse response = _mapper.Map<RoleGroupResponse>(entity);

            return response;
        }
    }
}
