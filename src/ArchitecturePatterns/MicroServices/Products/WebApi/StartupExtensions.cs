namespace Products.WebApi;

public static class StartupExtensions
{
    public static WebApplication UseDarkSwaggerUi(this WebApplication app)
    {
        // SwaggerDark.css source: https://dev.to/amoenus/turn-swagger-theme-to-the-dark-mode-4l5f
        app.UseSwaggerUI(c => c.InjectStylesheet("/swagger-ui/DarkSwagger.css"));
        app.MapGet("/swagger-ui/DarkSwagger.css", async (CancellationToken cancellationToken) =>
        {
            var css = await File.ReadAllBytesAsync("swagger-ui/DarkSwagger.css", cancellationToken);
            return Results.File(css, "text/css");
        }).ExcludeFromDescription();
        return app;
    }
}
