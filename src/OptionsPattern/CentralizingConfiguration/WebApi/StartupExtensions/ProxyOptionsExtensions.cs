using Microsoft.Extensions.Options;
using OptionsPattern.CentralizingConfiguration.WebApi.Options;

namespace OptionsPattern.CentralizingConfiguration.WebApi.StartupExtensions;

public static class ProxyOptionsExtensions
{
    public static void AddProxyOptions(this IServiceCollection services, string proxyName)
        => services
            .AddSingleton<IConfigureOptions<ProxyOptions>, ProxyOptions>()
            .AddSingleton<IValidateOptions<ProxyOptions>, ProxyOptions>()
            .AddSingleton(sp => sp.GetRequiredService<IOptions<ProxyOptions>>().Value)
            .Configure<ProxyOptions>(options => options.Name = proxyName)
            .AddOptions<ProxyOptions>()
            .ValidateOnStart();
}
