using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Role.V1.Commands.UpdateRole
{
    public class UpdateRoleV1CommandValidator : AbstractValidator<UpdateRoleV1Command>
    {
        public UpdateRoleV1CommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("required_field");
            RuleFor(x => x.RoleGroupId)
                .NotEmpty().WithMessage("required_field");
            RuleFor(x => x.WarehouseTypeId)
                .NotEmpty().WithMessage("required_field");

        }
    }
}
