using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.RoleGroup.Commands.CreateRoleGroup
{
    public class CreateRoleGroupCommandValidator : AbstractValidator<CreateRoleGroupCommand>
    {
        public CreateRoleGroupCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("required_field");
        }
    }
}