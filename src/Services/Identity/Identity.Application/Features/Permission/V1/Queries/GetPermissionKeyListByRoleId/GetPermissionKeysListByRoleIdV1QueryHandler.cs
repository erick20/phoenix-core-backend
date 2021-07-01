using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionKeyListByRoleId
{
    public class GetPermissionKeyListByRoleIdV1QueryHandler : IRequestHandler<GetPermissionKeyListByRoleIdV1Query, List<string>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetPermissionKeyListByRoleIdV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetPermissionKeyListByRoleIdV1QueryHandler(IMapper mapper, ILogger<GetPermissionKeyListByRoleIdV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<List<string>> Handle(GetPermissionKeyListByRoleIdV1Query request, CancellationToken cancellationToken)
        {
            List<string> keyList = await _unitOfWork.PermissionRepositoryV1.GetPermissionKeyListByRoleIdAsync(request.RoleId);

            return keyList;
         
        }
    }
}
