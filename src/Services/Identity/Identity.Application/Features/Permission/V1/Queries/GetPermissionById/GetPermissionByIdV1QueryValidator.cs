using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionById
{
    public class GetPermissionByIdV1QueryValidator : AbstractValidator<GetPermissionByIdV1Query>
    {
        public GetPermissionByIdV1QueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("required_field")
                .GreaterThan(0).WithMessage("must_greater_zero");

        }
    }
}
