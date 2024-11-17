using FluentValidation;
using Microsoft.Extensions.Options;

namespace OptionsPattern.OptionsFluentValidation.WebApi.Options;

public class FluentValidateOptions<TOptions>(IValidator<TOptions> validator) : IValidateOptions<TOptions>
    where TOptions : class
{
    private readonly IValidator<TOptions> _validator = validator;

    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        var validationResult = _validator.Validate(options);

        if (validationResult.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage);

        return ValidateOptionsResult.Fail(errorMessages);
    }
}
