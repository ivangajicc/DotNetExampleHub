/*
    The default sources, in order, are as follows:
    1. appsettings.json
    2. appsettings.{Environment}.json
    3. User secrets; these are only loaded when the environment is Development
    4. Environment variables
    5. Command-line arguments

Each next will overwrite previous if same config key.
 */
using Microsoft.Extensions.Options;
using OptionsPattern.CommonScenarios.WebApi;
using OptionsPattern.CommonScenarios.WebApi.Options;

var builder = WebApplication.CreateBuilder(args);

// Configure each time we inject options of MyOptions to have name as Default Option.
builder.Services.Configure<MyOptions>(options =>
{
    options.Name = "Default Option Hard Coded";
});

// Use options from appsettings.json (will override previously hard coded)
var defaultOptionsSection = builder.Configuration.GetSection("defaultOptions");
builder.Services.Configure<MyOptions>(defaultOptionsSection);

// Named options
builder.Services.Configure<MyOptions>(
    "Options1",
    builder.Configuration.GetSection("options1"));

builder.Services.Configure<MyOptions>(
    "Options2",
    builder.Configuration.GetSection("options2"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddNotificationService();

var app = builder.Build();

// Get default options
app.MapGet(
    "/my-options",
    (IOptionsMonitor<MyOptions> options) => options.CurrentValue);

// Get named options
// Factory each time we request options create them based on configuration (Transient)
app.MapGet(
    "/factory/{name}",
    (string name, IOptionsFactory<MyOptions> factory) => factory.Create(name));

// Monitor returns us already existing object (Singleton approach)
app.MapGet(
    "/monitor/{name}",
    (string name, IOptionsMonitor<MyOptions> monitor) => monitor.Get(name));

// Scoped lifetime
app.MapGet(
    "/snapshot/{name}",
    (string name, IOptionsSnapshot<MyOptions> snapshot) => snapshot.Get(name));

app.MapNotificationService();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
