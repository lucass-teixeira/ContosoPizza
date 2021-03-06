using MediatR;
using FluentValidation;
using System.Collections;

namespace PipelineBehavior
{
    public class ValidatorBehavior<TRequest, TResponse> : MediatR.IPipelineBehavior<TRequest, TResponse> where TRequest
    : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            var context = new ValidationContext<TRequest>(request);
            //Select all erros that are not null
            var failures = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors).Where(x => x != null).ToList();
            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
            return next();
        }
    }
}