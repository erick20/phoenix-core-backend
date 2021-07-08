using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.PermissionGroup.V1.Commands.CreatePermissionGroup
{
    public class CreatePermissionGroupV1CommandValidator : AbstractValidator<CreatePermissionGroupV1Command>
    {
        public CreatePermissionGroupV1CommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("required_field").WithName("name");
        }
    }
}
