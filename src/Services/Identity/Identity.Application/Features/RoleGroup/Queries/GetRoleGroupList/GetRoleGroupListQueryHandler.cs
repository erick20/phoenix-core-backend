using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Features.RolesGroup.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using entity = Identity.Domain.Entities;

namespace Identity.Application.Features.RoleGroup.Queries.GetRoleGroupList
{
    public class GetRoleGroupListQueryHandler : IRequestHandler<GetRoleGroupListQuery, List<RoleGroupResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleGroupListQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetRoleGroupListQueryHandler(IMapper mapper, ILogger<GetRoleGroupListQueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<RoleGroupResponse>> Handle(GetRoleGroupListQuery request, CancellationToken cancellationToken)
        {
            var entityList = _unitOfWork.RoleGroupRepositoryBase.GetNoTracking();

            List<RoleGroupResponse> respoonse = await _mapper.ProjectTo<RoleGroupResponse>(entityList).ToListAsync();

            return respoonse;
        }
    }
}
