using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.ToDoAggregate;
using CleanArchitecture.UseCases.Abstractions;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Todos.GetById;

public class GetTodoByIdHandler(IRepository<ToDo> repository) : IQueryHandler<GetTodoByIdQuery, TodoDto>
{
    public async Task<Result<TodoDto>> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (todo == null)
        {
            return Result<TodoDto>.NotFound();
        }

        return Result<TodoDto>.Success(new TodoDto(todo.Id, todo.IsResolved, todo.Description));
    }
}
