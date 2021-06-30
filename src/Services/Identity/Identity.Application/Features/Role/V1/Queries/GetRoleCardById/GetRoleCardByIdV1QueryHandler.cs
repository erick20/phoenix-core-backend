using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Role.V1.Queries.GetRoleCardById
{
    public class GetRoleCardByIdV1QueryHandler : IRequestHandler<GetRoleCardByIdV1Query, RoleLimitPermissionV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleCardByIdV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetRoleCardByIdV1QueryHandler(IMapper mapper, ILogger<GetRoleCardByIdV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<RoleLimitPermissionV1Response> Handle(GetRoleCardByIdV1Query request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.RoleRepositoryV1.GetRoleLimitPermissionByIdAsync(request.Id, false);

            if (role is null)
            {
                ProblemReporter.ReportResourseNotfound("not_found");
            }

            RoleLimitPermissionV1Response response = _mapper.Map<RoleLimitPermissionV1Response>(role);

            return response;
        }
    }
}
