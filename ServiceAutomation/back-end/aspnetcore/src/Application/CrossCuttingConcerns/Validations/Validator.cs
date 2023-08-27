using FluentValidation;

namespace Application.CrossCuttingConcerns.Validations;

public static class Validator
{
    public static async Task SingleValidatorValidateAsync(IValidator validator, object entity, CancellationToken cancellationToken = default)
    {
        await MultipleValidatorValidateAsync(new List<IValidator> { validator }.AsEnumerable(), entity, cancellationToken);
    }

    public static async Task MultipleValidatorValidateAsync(IEnumerable<IValidator> validators, object entity, CancellationToken cancellationToken = default)
    {
        var context = new ValidationContext<object>(entity);
        var results = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));
        var errors = results
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();

        if (errors.Any())
            throw new ValidationException(errors);
    }
}