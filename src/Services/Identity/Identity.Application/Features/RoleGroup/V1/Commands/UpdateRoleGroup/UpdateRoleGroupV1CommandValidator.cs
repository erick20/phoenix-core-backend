using FluentValidation;

namespace Identity.Application.Features.RoleGroup.V1.Commands.UpdateRoleGroup
{
    public class UpdateRoleGroupV1CommandValidator : AbstractValidator<UpdateRoleGroupV1Command>
    {
        public UpdateRoleGroupV1CommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("required_field");
        }
    }
}
