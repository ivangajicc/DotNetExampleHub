using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.ToDoAggregate;
using CleanArchitecture.UseCases.Abstractions;
using CleanArchitecture.UseCases.Todos;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Todos.List;

public class ListTodosHandler(IRepository<ToDo> repository)
  : IQueryHandler<ListTodosQuery, IEnumerable<TodoDto>>
{
    // If we need some much more complex query than basic repository fetches, we can create here in List folder interface for service
    // e.g. IListTodosRelatedToTopicQueryService and implement it inside Infrastructure/Database/QueryServices/ListTodosRelatedToTopicQueryService.cs.
    // We can in that service use any way to fetch data (ado, dapper, ef...)
    public async Task<Result<IEnumerable<TodoDto>>> Handle(ListTodosQuery request, CancellationToken cancellationToken)
        => Result<IEnumerable<TodoDto>>.Success((await repository.ListAsync(cancellationToken)).Select(todo => new TodoDto(todo.Id, todo.IsResolved, todo.Description)));
}
