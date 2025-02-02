using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Result;
using VerticalSliceArchitecture.Application.Abstractions;
using VerticalSliceArchitecture.Application.Domain.Todos;
using VerticalSliceArchitecture.Application.Infrastructure.Persistence;

namespace VerticalSliceArchitecture.Application.Features.Todos;

public class CreateTodoController : ApiControllerBase
{
    [HttpPost("/api/todos")]
    public async Task<IActionResult> CreateAsync(CreateTodo.CreateTodoDto todoDetails)
    {
        var result = await Mediator.Send(new CreateTodo.Command(todoDetails));

        if (result.Succeeded)
        {
            return Ok();
        }
        else
        {
            return BadRequest(result.Messages);
        }
    }
}

public abstract class CreateTodo
{
    public record CreateTodoDto(string Description);

    public record Command(CreateTodoDto TodoDetails) : ICommand;

    public class Handler(DatabaseContext databaseContext) : ICommandHandler<Command>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var newTodo = new ToDo(request.TodoDetails.Description);

            var createdTodo = await databaseContext.Set<ToDo>().AddAsync(newTodo, cancellationToken);

            databaseContext.SaveChanges();

            return createdTodo is not null ?
                Result.Success() :
                Result.Failure(new OperationResultMessage("Error while saving todo.", OperationResultSeverity.Error));
        }
    }

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator() => RuleFor(v => v.TodoDetails.Description)
                .MaximumLength(200)
                .NotEmpty();
    }
}
