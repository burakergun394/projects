using Application.CrossCuttingConcerns.Validations;
using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;

namespace Application.Behaviors.Validations;

internal class RequestValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Guard.Against.Null(next);
        if (!_validators.Any())
            return await next.Invoke();

        await Validator.MultipleValidatorValidateAsync(_validators, request, cancellationToken);
        return await next.Invoke();
    }
}