using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using entity = Identity.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.RoleGroup.V1.Queries.GetRolesGroupById
{
    public class GetRoleGroupByIdV1QueryHandler : IRequestHandler<GetRoleGroupByIdV1Query, RoleGroupV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleGroupByIdV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetRoleGroupByIdV1QueryHandler(IMapper mapper, ILogger<GetRoleGroupByIdV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<RoleGroupV1Response> Handle(GetRoleGroupByIdV1Query request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.RoleGroupRepositoryV1.GetRoleGroupByIdAsync(request.Id, false);

            if (entity is null)
            {
                ProblemReporter.ReportResourseNotfound("role_group_not_found");
            }

            RoleGroupV1Response response = _mapper.Map<RoleGroupV1Response>(entity);

            return response;
        }
    }
}
