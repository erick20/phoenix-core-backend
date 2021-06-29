using FluentValidation;

namespace Identity.Application.Features.RoleGroup.V1.Commands.CreateRoleGroup
{
    public class CreateRoleGroupV1CommandValidator : AbstractValidator<CreateRoleGroupV1Command>
    {
        public CreateRoleGroupV1CommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("required_field");
        }
    }
}