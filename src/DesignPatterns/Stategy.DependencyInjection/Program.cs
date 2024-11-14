using DesignPatterns.Strategy.DependencyInjection.Services.HelloWorldServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHelloWorldServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Strategy pattern combined with dependency injection. Based on desired language service provider will provide us with concrete implementation of
// the hello world service which allows us to switch behavior at a runtime.
// To add new language support we just create new class and implement IHelloWorldService without modifying existing code.
app.MapGet("/helloWorld/{languageCode}", (HelloWorldServiceProvider helloWorldServiceProvider, string languageCode) =>
{
    var helloWorldService = helloWorldServiceProvider.GetHelloWorldService(languageCode);

    return helloWorldService.GenerateHelloWorldMessage();
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();
