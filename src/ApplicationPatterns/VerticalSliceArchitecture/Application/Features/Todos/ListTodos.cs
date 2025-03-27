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
using VerticalSliceArchitecture.Application.Infrastructure.Persistence;

namespace VerticalSliceArchitecture.Application.Features.Todos;

public class ListTodosController : ApiControllerBase
{
    [HttpGet("/api/todos")]
    public async Task<IActionResult> ListAsync()
    {
        var result = await Mediator.Send(new ListTodos.Command());

        // We can create same Result handling extension for ApiControllerBase as we did for IEndPoint in clean architecture.
        if (result.Succeeded)
        {
            return Ok(result.Value);
        }
        else
        {
            return BadRequest(result.Messages);
        }
    }
}

public abstract class ListTodos
{
    public record ListTodosResponseDto(IEnumerable<TodoResponseDto> Todos);

    public record Command() : ICommand<ListTodosResponseDto>;

    public class Handler(DatabaseContext databaseContext) : ICommandHandler<Command, ListTodosResponseDto>
    {
        public async Task<Result<ListTodosResponseDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var todos = (await databaseContext.Set<ToDo>()
                                              .ToListAsync(cancellationToken)).ConvertAll(x => new TodoResponseDto(x.Id, x.IsResolved, x.Description));

            return Result<ListTodosResponseDto>.Success(new ListTodosResponseDto(todos));
        }
    }
}
