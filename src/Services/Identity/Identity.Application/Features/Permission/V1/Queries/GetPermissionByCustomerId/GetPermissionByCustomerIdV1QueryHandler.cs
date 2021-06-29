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

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionByCustomerId
{
    public class GetPermissionByCustomerIdV1QueryHandler : IRequestHandler<GetPermissionByCustomerIdV1Query, List<PermissionV1Response>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetPermissionByCustomerIdV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetPermissionByCustomerIdV1QueryHandler(IMapper mapper, ILogger<GetPermissionByCustomerIdV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Task<List<PermissionV1Response>> Handle(GetPermissionByCustomerIdV1Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
