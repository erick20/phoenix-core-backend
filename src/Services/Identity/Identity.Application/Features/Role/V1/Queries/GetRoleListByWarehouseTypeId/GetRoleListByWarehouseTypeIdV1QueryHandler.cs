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

namespace Identity.Application.Features.Role.V1.Queries.GetRoleListByWarehouseTypeId
{
    public class GetRoleListByWarehouseTypeIdV1QueryHandler : IRequestHandler<GetRoleListByWarehouseTypeIdV1Query, List<RoleLimitPermissionV1Response>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleListByWarehouseTypeIdV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetRoleListByWarehouseTypeIdV1QueryHandler(IMapper mapper, ILogger<GetRoleListByWarehouseTypeIdV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<List<RoleLimitPermissionV1Response>> Handle(GetRoleListByWarehouseTypeIdV1Query request, CancellationToken cancellationToken)
        {
            var role = _unitOfWork.RoleRepositoryV1.GetRoleLimitByWarehouseTypeIdAsync(request.WarehouseTypeId,false);

            List<RoleLimitPermissionV1Response> response = await _mapper.ProjectTo<RoleLimitPermissionV1Response>(role).ToListAsync();

            return response;
        }
    }
}
