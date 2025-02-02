using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Result;
using VerticalSliceArchitecture.Application.Abstractions;
using VerticalSliceArchitecture.Application.Domain.Todos;
using VerticalSliceArchitecture.Application.Domain.Todos.Events;
using VerticalSliceArchitecture.Application.Infrastructure.Persistence;

namespace VerticalSliceArchitecture.Application.Features.Todos;

public class GetTodoByIdController : ApiControllerBase
{
    [HttpGet("/api/todos/{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var result = await Mediator.Send(new GetTodoById.Query(id));

        if (result.Succeeded)
        {
            return Ok(result.Value);
        }
        else if (result is NotFoundResult<TodoResponseDto>)
        {
            return NotFound();
        }
        else
        {
            return BadRequest(result.Messages);
        }
    }
}

public abstract class GetTodoById
{
    public record Query(Guid Id) : IQuery<TodoResponseDto>;

    public class Handler(DatabaseContext databaseContext) : IQueryHandler<Query, TodoResponseDto>
    {
        public async Task<Result<TodoResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var toDo = await databaseContext.Set<ToDo>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (toDo == null)
            {
                return Result<TodoResponseDto>.NotFound();
            }

            return Result<TodoResponseDto>.Success(new TodoResponseDto(toDo.Id, toDo.IsResolved, toDo.Description));
        }
    }

    internal sealed class Validator : AbstractValidator<Query>
    {
        public Validator() => RuleFor(v => v.Id).NotEmpty();
    }
}
