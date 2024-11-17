using Microsoft.Extensions.Options;

namespace OptionsPattern.ConfigurationGenerators.WebApi.Options;

[OptionsValidator]
public partial class MyOptionsValidator : IValidateOptions<MyOptions>
{
}
