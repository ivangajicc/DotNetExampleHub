using Microsoft.Extensions.Options;

namespace OptionsPattern.OptionsConfiguration.WebApi.Options.Configuration;

// This class will be triggered only for unnamed (default) ConfigureMeOptions.
public class ConfigureMoreConfigureMeOptions :
    IConfigureOptions<ConfigureMeOptions>
{
    public void Configure(ConfigureMeOptions options)
        => options.Lines = options.Lines.Append("ConfigureMore:Configure");
}
