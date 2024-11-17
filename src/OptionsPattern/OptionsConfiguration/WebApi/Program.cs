using Microsoft.Extensions.Options;
using OptionsPattern.OptionsConfiguration.WebApi.Options;
using OptionsPattern.OptionsConfiguration.WebApi.Options.Configuration;

const string OptionsName = "NamedOptions";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services
    .AddSingleton<IConfigureOptions<ConfigureMeOptions>, ConfigureAllConfigureMeOptions>()
    .AddSingleton<IConfigureOptions<ConfigureMeOptions>, ConfigureMoreConfigureMeOptions>()
    .AddSingleton<IPostConfigureOptions<ConfigureMeOptions>, ConfigureAllConfigureMeOptions>();

builder.Services
    .Configure<ConfigureMeOptions>(
        builder.Configuration.GetSection("configureMe"))
    .Configure<ConfigureMeOptions>(
        OptionsName,
        builder.Configuration.GetSection("configureMe"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet(
    "/configure-me",
    (IOptionsMonitor<ConfigureMeOptions> options) => new
    {
        DefaultInstance = options.CurrentValue,
        NamedInstance = options.Get(OptionsName),
    });

app.Run();
