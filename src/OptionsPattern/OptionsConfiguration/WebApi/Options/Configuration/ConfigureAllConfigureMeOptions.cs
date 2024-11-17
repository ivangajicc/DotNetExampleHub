using Microsoft.Extensions.Options;

namespace OptionsPattern.OptionsConfiguration.WebApi.Options.Configuration;

// This class will be triggered for all ConfigureMeOptions registrations (named and unnamed)
public class ConfigureAllConfigureMeOptions :
    IConfigureNamedOptions<ConfigureMeOptions>,
    IPostConfigureOptions<ConfigureMeOptions>
{
    public void Configure(string? name, ConfigureMeOptions options)
    {
        options.Lines =
            options.Lines.Append($"ConfigureAll: Configure name: {name}");

        if (!string.IsNullOrEmpty(name))
        {
            options.Lines = options.Lines.Append(
                $"ConfigureAll:Configure Not Default: {name}");
        }
    }

    public void Configure(ConfigureMeOptions options) => Configure(string.Empty, options);

    public void PostConfigure(string? name, ConfigureMeOptions options)
        => options.Lines = options.Lines.Append($"ConfigureAll: PostConfigure name: {name}");
}
