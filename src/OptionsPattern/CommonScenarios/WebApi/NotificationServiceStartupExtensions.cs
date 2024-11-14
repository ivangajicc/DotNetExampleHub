using System;
using Microsoft.Extensions.Options;
using OptionsPattern.CommonScenarios.WebApi.Options;

namespace OptionsPattern.CommonScenarios.WebApi;

public static class NotificationServiceStartupExtensions
{
    public static WebApplicationBuilder AddNotificationService(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(nameof(EmailOptions)));
        builder.Services.AddSingleton<NotificationService>();
        return builder;
    }

    public static WebApplication MapNotificationService(this WebApplication app)
    {
        app.MapPost("notify/{email}", async (string email, NotificationService service) =>
        {
            await service.NotifyAsync(email);
            return Results.Ok();
        });
        app.MapPut("notify", (NotificationService service) =>
        {
            service.StartListeningForChanges();
            return Results.Ok();
        });
        app.MapDelete("notify", (NotificationService service) =>
        {
            service.StopListeningForChanges();
            return Results.Ok();
        });
        return app;
    }
}
