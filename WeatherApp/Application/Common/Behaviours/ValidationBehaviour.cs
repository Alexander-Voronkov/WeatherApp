using FluentValidation;
using MediatR;

namespace Application.Common.Behaviours;
internal class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var validationResults = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(request, cancellationToken)));

        var isValid = Array.TrueForAll(validationResults, r => r.IsValid);

        if (!isValid)
        {
            var errors = validationResults
                .Where(r => !r.IsValid)
                .SelectMany(r => r.Errors.Select(x => x.ErrorMessage));

            throw new ValidationException(string.Join(" ;", errors));
        }

        return await next();
    }
}
