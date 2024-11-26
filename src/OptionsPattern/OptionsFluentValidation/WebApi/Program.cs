using FluentValidation;
using Microsoft.Extensions.Options;
using OptionsPattern.OptionsFluentValidation.WebApi.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSingleton<IValidator<MyOptions.Data>, MyOptions.Validator>()
    .AddSingleton<IValidateOptions<MyOptions.Data>, FluentValidateOptions<MyOptions.Data>>();

builder.Services
    .AddOptions<MyOptions.Data>()
    .BindConfiguration("MyOptions")
    .ValidateOnStart();

var app = builder.Build();

app.MapGet("/", (IOptionsMonitor<MyOptions.Data> optionsMonitor) => optionsMonitor.CurrentValue.Name);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
