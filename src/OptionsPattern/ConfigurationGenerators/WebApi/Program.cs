using Microsoft.Extensions.Options;
using OptionsPattern.ConfigurationGenerators.WebApi.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<MyOptions>("Valid")
                .BindConfiguration("MyOptions")
                .ValidateOnStart();

builder.Services.AddSingleton<IValidateOptions<MyOptions>, MyOptionsValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
