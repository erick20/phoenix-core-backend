using FluentValidation;

namespace Identity.Application.Features.RoleGroup.V1.Queries.GetRolesGroupById
{
    public class GetRoleGroupByIdV1QueryValidator : AbstractValidator<GetRoleGroupByIdV1Query>
    {
        public GetRoleGroupByIdV1QueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("required_field")
                .GreaterThan(0).WithMessage("must_greater_zero");

        }
    }
}
