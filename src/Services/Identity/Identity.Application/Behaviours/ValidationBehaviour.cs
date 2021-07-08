using FluentValidation;
using Identity.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    List<ErrorValidationModel> errorValidations = new List<ErrorValidationModel>();
                    foreach (var item in failures)
                    {
                        errorValidations.Add(new ErrorValidationModel
                        {
                            Key = item.ErrorMessage,
                            Name = item.FormattedMessagePlaceholderValues["PropertyName"].ToString()
                        });
                    }

                    
                    throw ProblemReporter.ReturnBadRequest(errorValidations);//new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
