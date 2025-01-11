using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.ToDoAggregate;
using CleanArchitecture.Core.ToDoAggregate.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Core.Extensions;

public static class Startup
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IResolveTodoService, ResolveTodoService>();

        return services;
    }
}
