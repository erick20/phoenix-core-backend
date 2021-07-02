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

namespace Identity.Application.Features.Permission.V1.Commands.CheckCustomerPermission
{
    public class CheckCustomerPermissionV1CommandHandler : IRequestHandler<CheckCustomerPermissionV1Command, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CheckCustomerPermissionV1CommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CheckCustomerPermissionV1CommandHandler(IMapper mapper, ILogger<CheckCustomerPermissionV1CommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Unit> Handle(CheckCustomerPermissionV1Command request, CancellationToken cancellationToken)
        {
            var rolePermission = await _unitOfWork.RolePermissionRepositoryV1.GetByRoleIdActionNameAsync(request.RoleId, request.ActionName, false);

            if (rolePermission is null)
            {
                //AuthorizationErrorModel errorModel = new AuthorizationErrorModel()
                //{
                //    Key = "access_denied",
                //};
                //ProblemReporter.ReportForbiddenError(JsonConvert.SerializeObject(errorModel));
            }
            //}
            //else
            //{
            //    // Hardcode here for action "RegisterCustomerInfo", only access this action
            //    if (model.ActionName != "RegisterCustomerInfo")
            //    {
            //        ProblemReporter.ReportForbiddenError("dont have access to this");
            //    }
            //}

            return Unit.Value;
        }
    }
}


// old handler
////if (model.MustBeActive)
////{
//if (userContextModel.CustomerState != CustomerStateEnum.Activated)
//{
//    if (model.ActionName != "SignUpWithSocial")
//    {
//        if (model.ActionName != "SetPickUpLocation")
//        {
//            AuthorizationErrorModel errorModel = new AuthorizationErrorModel()
//            {
//                Key = "access_denied",
//            };
//            ProblemReporter.ReportForbiddenError(JsonConvert.SerializeObject(errorModel));
//        }

//    }
//}

//bool hasPermission = await _unitOfWork.RoleRepositoryBase.HasPermission(userContextModel.RoleId, model.ActionName);

//if (!hasPermission)
//{
//    //AuthorizationErrorModel errorModel = new AuthorizationErrorModel()
//    //{
//    //    Key = "access_denied",
//    //};
//    //ProblemReporter.ReportForbiddenError(JsonConvert.SerializeObject(errorModel));
//}
////}
////else
////{
////    // Hardcode here for action "RegisterCustomerInfo", only access this action
////    if (model.ActionName != "RegisterCustomerInfo")
////    {
////        ProblemReporter.ReportForbiddenError("dont have access to this");
////    }
////}