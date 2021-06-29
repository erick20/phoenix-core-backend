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

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionById
{
    public class GetPermissionByIdV1QueryHandler : IRequestHandler<GetPermissionByIdV1Query, PermissionV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetPermissionByIdV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetPermissionByIdV1QueryHandler(IMapper mapper, ILogger<GetPermissionByIdV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<PermissionV1Response> Handle(GetPermissionByIdV1Query request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.PermissionRepositoryV1.GetPermissionByIdAsync(request.Id, false);

            if (entity is null)
            {
                ProblemReporter.ReportResourseNotfound("not_found");
            }

            PermissionV1Response response = _mapper.Map<PermissionV1Response>(entity);

            return response;
        }
    }
}
