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

namespace CleanArchitecture.UseCases.Todos.Create;

public class CreateTodoHandler(IRepository<ToDo> repository) : ICommandHandler<CreateTodoCommand, TodoDto>
{
    public async Task<Result<TodoDto>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var newTodo = new ToDo(request.Description);

        var createdTodo = await repository.AddAsync(newTodo, cancellationToken);

        return createdTodo is not null ?
            Result<TodoDto>.Success(new TodoDto(createdTodo.Id, createdTodo.IsResolved, createdTodo.Description)) :
            Result<TodoDto>.Failure(new OperationResultMessage("Error while saving todo.", OperationResultSeverity.Error));
    }
}
