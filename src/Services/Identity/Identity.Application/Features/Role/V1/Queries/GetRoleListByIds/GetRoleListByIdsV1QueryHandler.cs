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

namespace Identity.Application.Features.Role.V1.Queries.GetRoleListByIds
{
    public class GetRoleListByIdsV1QueryHandler : IRequestHandler<GetRoleListByIdsV1Query, List<RoleLimitPermissionV1Response>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleListByIdsV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetRoleListByIdsV1QueryHandler(IMapper mapper, ILogger<GetRoleListByIdsV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<List<RoleLimitPermissionV1Response>> Handle(GetRoleListByIdsV1Query request, CancellationToken cancellationToken)
        {
            var roleList = _unitOfWork.RoleRepositoryV1.GetRoleListByIds(request.Ids);

            List<RoleLimitPermissionV1Response> response = await _mapper.ProjectTo<RoleLimitPermissionV1Response>(roleList).ToListAsync();

            return response;
        }
    }
}
