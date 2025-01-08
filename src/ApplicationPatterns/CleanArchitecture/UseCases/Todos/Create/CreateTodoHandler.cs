using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.ToDoAggregate;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Todos.Create;

public class CreateTodoHandler(IRepository<ToDo> repository) : IRequestHandler<CreateTodoCommand, Result<TodoDto>>
{
    public async Task<Result<TodoDto>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var newTodo = new ToDo(request.Description);

        var createdTodo = await repository.AddAsync(newTodo, cancellationToken);

        return createdTodo is not null ?
            ResultFactory.Success(new TodoDto(createdTodo.IsResolved, createdTodo.Description)) :
            ResultFactory.Failure(new OperationResultMessage("Error while saving todo.", OperationResultSeverity.Error));
    }
}
