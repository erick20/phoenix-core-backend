using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using entities = Identity.Domain.Entities;

namespace Identity.Application.Features.Permission.V1.Commands.CreatePermission
{
    public class CreatePermissionV1CommandHandler : IRequestHandler<CreatePermissionV1Command, PermissionV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePermissionV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePermissionV1CommandHandler(IMapper mapper, ILogger<CreatePermissionV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<PermissionV1Response> Handle(CreatePermissionV1Command request, CancellationToken cancellationToken)
        {
            var group = await _unitOfWork.PermissionGroupRepositoryV1.GetPermissionGroupByIdAsync(request.Id, false);

            if (group is null)
            {
                ProblemReporter.ReportResourseNotfound("group_not_found");
            }

            var entity = _mapper.Map<entities.Permission>(request);

            _unitOfWork.PermissionRepositoryV1.Add(entity);

            _unitOfWork.SaveChanges();

            PermissionV1Response response = _mapper.Map<PermissionV1Response>(entity);

            return response;
        }
    }
}
