using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using entities = Identity.Domain.Entities;

namespace Identity.Application.Features.Permission.V1.Commands.UpdatePermission
{
    public class UpdatePermissionV1CommandHandler : IRequestHandler<UpdatePermissionV1Command, PermissionV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePermissionV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePermissionV1CommandHandler(IMapper mapper, ILogger<UpdatePermissionV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<PermissionV1Response> Handle(UpdatePermissionV1Command request, CancellationToken cancellationToken)
        {
            var group = await _unitOfWork.PermissionGroupRepositoryV1.GetPermissionGroupByIdAsync(request.Id, false);

            if (group is null)
            {
                ProblemReporter.ReportResourseNotfound("group_not_found");
            }

            var entity = await _unitOfWork.PermissionRepositoryV1.GetPermissionByIdAsync(request.Id ,true);

            if (entity is null)
            {
                ProblemReporter.ReportResourseNotfound("entity_not_found");
            }

            entity = _mapper.Map<entities.Permission>(request);

            _unitOfWork.PermissionRepositoryV1.Update(entity);

            _unitOfWork.SaveChanges();

            PermissionV1Response response = _mapper.Map<PermissionV1Response>(entity);

            return response;
        }
    }
}
