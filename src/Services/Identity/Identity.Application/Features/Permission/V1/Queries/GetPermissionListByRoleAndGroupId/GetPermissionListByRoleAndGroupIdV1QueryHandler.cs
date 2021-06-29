using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionListByRoleAndGroupId
{
    public class GetPermissionListByRoleAndGroupIdV1QueryHandler : IRequestHandler<GetPermissionListByRoleAndGroupIdV1Query, List<HasPermissionV1Response>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetPermissionListByRoleAndGroupIdV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetPermissionListByRoleAndGroupIdV1QueryHandler(IMapper mapper, ILogger<GetPermissionListByRoleAndGroupIdV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<List<HasPermissionV1Response>> Handle(GetPermissionListByRoleAndGroupIdV1Query request, CancellationToken cancellationToken)
        {
            var entityList = _unitOfWork.PermissionRepositoryV1.GetPermissionByGroupId(request.GroupId).Include(x => x.RolePermissions);

            List<HasPermissionV1Response> response = await _mapper.ProjectTo<HasPermissionV1Response>(entityList).ToListAsync();

            response.ForEach(x => {
                if (x.RoleIds.Contains(request.RoleId))
                {
                    x.HasPermission = true;
                }
            });

            return response;
        }
    }
}
