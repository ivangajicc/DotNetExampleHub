using Utilities.Mapperly.Core.Dto;
using Utilities.Mapperly.Core.Mappers;
using Utilities.Mapperly.Core.Mappers.Interfaces;
using Utilities.Mapperly.Core.Repositories;
using Utilities.Mapperly.FakeInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddScoped<ITodoRepository, TodoRepository>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
;

builder.Services.AddSingleton<ITodoMapper, TodoMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/todos/DependencyInjectionOrientedMapping", async (ITodoRepository todoRepository, ITodoMapper mapper, CancellationToken cancellationToken) =>
{
    var todos = await todoRepository.AllAsync(cancellationToken);
    return todos.Select(todo => mapper.MapToDto(todo));
}).Produces(200, typeof(TodoDto[]));

app.MapGet("/todos/ExtensionMethodOrientedMapping", async (ITodoRepository todoRepository, CancellationToken cancellationToken) =>
{
    var todos = await todoRepository.AllAsync(cancellationToken);
    return todos.Select(todo => todo.MapToDto());
}).Produces(200, typeof(TodoDto[]));

app.Run();
