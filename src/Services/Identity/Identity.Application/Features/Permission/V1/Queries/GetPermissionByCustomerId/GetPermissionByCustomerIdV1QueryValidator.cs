using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Permission.V1.Queries.GetPermissionByCustomerId
{
    public class GetPermissionByCustomerIdV1QueryValidator : AbstractValidator<GetPermissionByCustomerIdV1Query>
    {
        public GetPermissionByCustomerIdV1QueryValidator()
        {
            RuleFor(p => p.CustomerId)
                .NotEmpty().WithMessage("required_field")
                .GreaterThan(0).WithMessage("must_greater_zero");

        }
    }
}
