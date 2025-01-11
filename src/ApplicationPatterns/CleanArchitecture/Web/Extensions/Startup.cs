using System;
using System.Reflection;
using CleanArchitecture.Core.ToDoAggregate;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.UseCases.Todos.Create;
using MediatR;

namespace CleanArchitecture.Web.Extensions;

public static class Startup
{
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        Assembly[] mediatRAssemblies = [ Assembly.GetAssembly(typeof(ToDo))!, // Core
        Assembly.GetAssembly(typeof(CreateTodoCommand))!, // UseCases
        ];

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));

        return services;
    }

    public static void SeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<DatabaseContext>();
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
        }
    }
}
