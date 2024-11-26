using DesignPatterns.DecoratorUsingScrutor;
using DesignPatterns.DecoratorUsingScrutor.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IComponent, ComponentA>()
                .Decorate<IComponent, DecoratorA>()
                .Decorate<IComponent, DecoratorB>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", (IComponent component) => component.Operation());

app.Run();

public abstract partial class Program;
