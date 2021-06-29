using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.RoleGroup.V1.Queries.GetRoleGroupList
{
    public class GetRoleGroupListV1QueryHandler : IRequestHandler<GetRoleGroupListV1Query, List<RoleGroupV1Response>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleGroupListV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetRoleGroupListV1QueryHandler(IMapper mapper, ILogger<GetRoleGroupListV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<RoleGroupV1Response>> Handle(GetRoleGroupListV1Query request, CancellationToken cancellationToken)
        {
            var entityList = _unitOfWork.RoleGroupRepositoryBase.GetNoTracking();

            List<RoleGroupV1Response> respoonse = await _mapper.ProjectTo<RoleGroupV1Response>(entityList).ToListAsync();

            return respoonse;
        }
    }
}
