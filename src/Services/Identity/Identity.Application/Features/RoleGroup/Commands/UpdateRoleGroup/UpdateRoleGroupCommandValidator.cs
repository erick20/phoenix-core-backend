using FluentValidation;
using Identity.Application.Features.RoleGroup.Commands.UpdateRoleGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.RoleGroup.Commands.UpdateRoleGroup
{
    public class UpdateRoleGroupCommandValidator : AbstractValidator<UpdateRoleGroupCommand>
    {
        public UpdateRoleGroupCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("required_field");
        }
    }
}
