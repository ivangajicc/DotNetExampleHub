using Microsoft.OpenApi.Models;
using REPR.WebApi;
using REPR.WebApi.Features;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.CustomSchemaIds(type => type.FullName?.Replace("+", ".")));
builder.AddExceptionMapper();
builder.AddFeatures();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDarkSwaggerUi();
}

app.UseExceptionMapper();
app.MapFeatures();
await app.SeedFeaturesAsync();

app.Run();

public partial class Program { }
