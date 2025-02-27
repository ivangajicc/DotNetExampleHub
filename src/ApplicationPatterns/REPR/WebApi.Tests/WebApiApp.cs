using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using REPR.WebApi.Features;
using static REPR.WebApi.Features.Baskets.Baskets;
using static REPR.WebApi.Features.Products.Products;

namespace REPR.WebApi.Tests;

public class WebApiApp : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? _afterConfigureServices;
    private readonly string _databaseName;

    public WebApiApp([CallerMemberName] string? databaseName = null, Action<IServiceCollection>? afterConfigureServices = null)
    {
        _databaseName = databaseName ?? nameof(WebApiApp);

        // Add some randomness to the database name to ensure uniqueness
        // for test methods that have the same name.
        _databaseName += Guid.NewGuid().ToString();
        _afterConfigureServices = afterConfigureServices;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Override the default DbContext options to make
            // a different InMemory database per test case so there is no
            // seed conflicts.
            services
                .AddScoped(ConfigureContext<BasketContext>)
                .AddScoped(ConfigureContext<ProductContext>)
            ;
            _afterConfigureServices?.Invoke(services);
        });
        return base.CreateHost(builder);
    }

    public DbContextOptions<TDbContext> ConfigureContext<TDbContext>(IServiceProvider sp)
        where TDbContext : DbContext => new DbContextOptionsBuilder<TDbContext>()
            .UseInMemoryDatabase(_databaseName + typeof(TDbContext).Name)
            .UseApplicationServiceProvider(sp)
            .Options;

    public async Task SeedAsync<TDbContext>(Func<TDbContext, Task> seeder)
        where TDbContext : DbContext
    {
        using var seedScope = Services.CreateScope();
        var db = seedScope.ServiceProvider.GetRequiredService<TDbContext>();
        await seeder(db);
    }
}
