using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionListByGroupId
{
    public class GetPermissionListByGroupIdV1QueryHandler : IRequestHandler<GetPermissionListByGroupIdV1Query, List<PermissionV1Response>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetPermissionListByGroupIdV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetPermissionListByGroupIdV1QueryHandler(IMapper mapper, ILogger<GetPermissionListByGroupIdV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<PermissionV1Response>> Handle(GetPermissionListByGroupIdV1Query request, CancellationToken cancellationToken)
        {
            var entityList = _unitOfWork.PermissionRepositoryV1.GetPermissionByGroupId(request.GroupId);

            List<PermissionV1Response> response = await _mapper.ProjectTo<PermissionV1Response>(entityList).ToListAsync();

            return response;
        }
    }
}
