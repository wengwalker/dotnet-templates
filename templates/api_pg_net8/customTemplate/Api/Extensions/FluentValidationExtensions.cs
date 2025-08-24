using FluentValidation;

namespace Api.Api.Extensions;

public static class FluentValidationExtensions
{
    public static async Task ValidateAndThrowExceptionAsync<T>(this IValidator<T> validator, T instance, string exceptionMessage, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(instance, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(exceptionMessage, validationResult.Errors);
        }
    }
}
