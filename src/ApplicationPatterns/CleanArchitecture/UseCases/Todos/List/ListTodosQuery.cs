using CleanArchitecture.UseCases.Abstractions;
using CleanArchitecture.UseCases.Todos;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Todos.List;

public record ListTodosQuery(int? Skip, int? Take) : IQuery<IEnumerable<TodoDto>>;
