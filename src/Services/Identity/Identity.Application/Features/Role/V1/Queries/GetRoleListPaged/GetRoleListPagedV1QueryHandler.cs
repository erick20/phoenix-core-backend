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

namespace Identity.Application.Features.Role.V1.Queries.GetRoleListPaged
{
    public class GetRoleListPagedV1QueryHandler : IRequestHandler<GetRoleListPagedV1Query, PagedResponse<RoleV1Response>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleListPagedV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetRoleListPagedV1QueryHandler(IMapper mapper, ILogger<GetRoleListPagedV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<PagedResponse<RoleV1Response>> Handle(GetRoleListPagedV1Query request, CancellationToken cancellationToken)
        {
            var entityList = await _unitOfWork.RoleRepositoryV1.GetPagedRoleListAsync(request.Pn, request.Ps);

            PagedResponse<RoleV1Response> response = _mapper.Map<PagedResponse<RoleV1Response>>(entityList);

            return response;
        }
    }
}
