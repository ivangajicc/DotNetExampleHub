using Utilities.MappingService.Core.Dto;
using Utilities.MappingService.Core.Mappers;
using Utilities.MappingService.Core.Models;
using Utilities.MappingService.Core.Repositories;
using Utilities.MappingService.FakeInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<ITodoRepository, TodoRepository>()
    .AddSingleton<IMapper<Todo, TodoDto>, TodoMapper>()
    .AddSingleton<IMappingService, ServiceLocatorMappingService>()

    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/todos", async (ITodoRepository todoRepository, IMappingService mapper, CancellationToken cancellationToken) =>
{
    var todos = await todoRepository.AllAsync(cancellationToken);
    return todos.Select(todo => mapper.Map<Todo, TodoDto>(todo));
}).Produces(200, typeof(TodoDto[]));

app.Run();
