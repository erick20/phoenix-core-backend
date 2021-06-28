using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.RolesGroup.Queries.GetRolesGroupById
{
    public class GetRoleGroupByIdQueryValidator : AbstractValidator<GetRoleGroupByIdQuery>
    {
        public GetRoleGroupByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("required_field")
                .GreaterThan(0).WithMessage("must_greater_zero");

        }
    }
}
