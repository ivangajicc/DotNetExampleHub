using AutoMapper;
using Utilities.AutoMapper.Core.Dto;
using Utilities.AutoMapper.Core.MappingProfiles;
using Utilities.AutoMapper.Core.Models;
using Utilities.AutoMapper.Core.Repositories;
using Utilities.AutoMapper.FakeInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(TodoMappingProfile).Assembly);

builder.Services
    .AddScoped<ITodoRepository, TodoRepository>()
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

app.MapGet("/todos", async (ITodoRepository todoRepository, IMapper mapper, CancellationToken cancellationToken) =>
{
    var todos = await todoRepository.AllAsync(cancellationToken);
    return todos.Select(todo => mapper.Map<TodoDto>(todo));
}).Produces(200, typeof(TodoDto[]));

app.Run();
