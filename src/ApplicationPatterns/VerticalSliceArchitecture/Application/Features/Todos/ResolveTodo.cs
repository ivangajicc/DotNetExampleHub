using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Result;
using VerticalSliceArchitecture.Application.Abstractions;
using VerticalSliceArchitecture.Application.Domain.Todos;
using VerticalSliceArchitecture.Application.Domain.Todos.Events;
using VerticalSliceArchitecture.Application.Infrastructure.Persistence;

namespace VerticalSliceArchitecture.Application.Features.Todos;

public class ResolveTodoController : ApiControllerBase
{
    [HttpPatch("/api/todos/{id}/resolve")]
    public async Task<IActionResult> ResolveAsync(Guid id)
    {
        var result = await Mediator.Send(new ResolveTodo.Command(id));

        // We can create same Result handling extension for ApiControllerBase as we did for IEndPoint in clean architecture.
        if (result.Succeeded)
        {
            return Ok();
        }
        else if (result is SharedKernel.Result.NotFoundResult)
        {
            return NotFound();
        }
        else
        {
            return BadRequest(result.Messages);
        }
    }
}

public abstract class ResolveTodo
{
    public record Command(Guid Id) : ICommand;

    public class Handler(DatabaseContext databaseContext, IMediator mediator) : ICommandHandler<Command>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var toDo = await databaseContext.Set<ToDo>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (toDo == null)
            {
                return Result.NotFound();
            }

            toDo.Resolve();

            databaseContext.Set<ToDo>().Update(toDo);
            databaseContext.SaveChanges();

            var @event = new TodoResolvedEvent(toDo.Id);

            await mediator.Publish(@event, cancellationToken);

            return Result.Success();
        }
    }

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator() => RuleFor(v => v.Id).NotEmpty();
    }
}
