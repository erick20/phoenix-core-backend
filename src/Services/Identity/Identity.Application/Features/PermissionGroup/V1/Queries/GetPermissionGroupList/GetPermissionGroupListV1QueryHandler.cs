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


namespace Identity.Application.Features.PermissionGroup.V1.Queries.GetPermissionGroupList
{
    public class GetPermissionGroupListV1QueryHandler : IRequestHandler<GetPermissionGroupListV1Query, List<PermissionGroupV1Response>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetPermissionGroupListV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetPermissionGroupListV1QueryHandler(IMapper mapper, ILogger<GetPermissionGroupListV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<PermissionGroupV1Response>> Handle(GetPermissionGroupListV1Query request, CancellationToken cancellationToken)
        {
            var entityList = _unitOfWork.PermissionGroupRepositoryV1.GetNoTracking();

            List<PermissionGroupV1Response> respoonse = await _mapper.ProjectTo<PermissionGroupV1Response>(entityList).ToListAsync();

            return respoonse;
        }
    }
}
