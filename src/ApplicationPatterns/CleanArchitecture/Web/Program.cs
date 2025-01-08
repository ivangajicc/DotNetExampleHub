using CleanArchitecture.Infrastructure.Extensions;
using CleanArchitecture.Web.Extensions;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterInfrastructure(builder.Configuration)
    .AddMediatrConfigs();

builder.Services.AddFastEndpoints()
                .SwaggerDocument(o => o.ShortSchemaNames = true);

var app = builder.Build();

app.UseFastEndpoints()
    .UseSwaggerGen();

app.SeedDatabase();

app.Run();
