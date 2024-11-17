using Microsoft.Extensions.Options;
using OptionsPattern.CentralizingConfiguration.WebApi.Options;
using OptionsPattern.CentralizingConfiguration.WebApi.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddProxyOptions("High-speed proxy");

var app = builder.Build();

app.MapGet("/", (ProxyOptions options) => options);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
