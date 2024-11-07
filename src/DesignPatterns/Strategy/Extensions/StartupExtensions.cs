using System.Text.Json.Serialization;

namespace DesignPatterns.Strategy.Extensions;

public static class StartupExtensions
{
    public static WebApplicationBuilder UseStringSerializationForEnums(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
            opt => opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(
            options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        return builder;
    }
}
