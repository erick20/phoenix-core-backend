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

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionList
{
    public class GetPermissionListV1QueryHandler : IRequestHandler<GetPermissionListV1Query, List<PermissionV1Response>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetPermissionListV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetPermissionListV1QueryHandler(IMapper mapper, ILogger<GetPermissionListV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<PermissionV1Response>> Handle(GetPermissionListV1Query request, CancellationToken cancellationToken)
        {
            var entityList = _unitOfWork.PermissionRepositoryV1.GetNoTracking();

            List<PermissionV1Response> respoonse = await _mapper.ProjectTo<PermissionV1Response>(entityList).ToListAsync();

            return respoonse;
        }
    }
}
