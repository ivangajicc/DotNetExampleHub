using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.ToDoAggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Result;

namespace CleanArchitecture.Core.ToDoAggregate.Services;

public class ResolveTodoService(
    IMediator mediator,
    IRepository<ToDo> repository) : IResolveTodoService
{
    public async Task<Result> ResolveTodoAsync(Guid todoId, CancellationToken cancellationToken)
    {
        var toDo = await repository.GetByIdAsync(todoId, cancellationToken);

        if (toDo == null)
        {
            return Result.NotFound();
        }

        toDo.Resolve();

        await repository.UpdateAsync(toDo, cancellationToken);

        var @event = new TodoResolvedEvent(todoId);

        await mediator.Publish(@event, cancellationToken);

        return Result.Success();
    }
}
