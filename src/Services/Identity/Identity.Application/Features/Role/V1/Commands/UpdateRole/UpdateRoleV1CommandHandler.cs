using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using entities = Identity.Domain.Entities;

namespace Identity.Application.Features.Role.V1.Commands.UpdateRole
{
    public class UpdateRoleV1CommandHandler : IRequestHandler<UpdateRoleV1Command, RoleLimitPermissionV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoleV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateRoleV1CommandHandler(IMapper mapper, ILogger<UpdateRoleV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<RoleLimitPermissionV1Response> Handle(UpdateRoleV1Command request, CancellationToken cancellationToken)
        {
            var roleGroupEntity = await _unitOfWork.RoleGroupRepositoryV1.GetRoleGroupByIdAsync(request.RoleGroupId, false);

            if (roleGroupEntity is null)
            {
                ProblemReporter.ReportResourseNotfound("group_not_found");
            }

            var oldEntity = await _unitOfWork.RoleRepositoryV1.GetRoleLimitByIdAsync(request.Id ,true);

            if (oldEntity is null)
            {
                ProblemReporter.ReportResourseNotfound("role_not_found");
            }

            var entity = _mapper.Map<entities.Role>(request);

            _unitOfWork.RoleRepositoryV1.Delete(oldEntity);

            _unitOfWork.RoleRepositoryV1.Add(entity);

            _unitOfWork.SaveChanges();

            RoleLimitPermissionV1Response response = _mapper.Map<RoleLimitPermissionV1Response>(entity);

            return response;
        }
    }
}
