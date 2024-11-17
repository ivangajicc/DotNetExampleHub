using FluentValidation;

namespace OptionsPattern.OptionsFluentValidation.WebApi.Options;

public abstract class MyOptions
{
    public class Validator : AbstractValidator<Data>
    {
        public Validator() => RuleFor(x => x.Name).NotEmpty();
    }

    public class Data
    {
        public string? Name { get; set; }
    }
}
