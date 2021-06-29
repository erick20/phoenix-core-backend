using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.PermissionGroup.V1.Commands.UpdatePermissionGroup
{
    public class UpdatePermissionGroupV1CommandValidator : AbstractValidator<UpdatePermissionGroupV1Command>
    {
        public UpdatePermissionGroupV1CommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("required_field");
        }
    }
}
