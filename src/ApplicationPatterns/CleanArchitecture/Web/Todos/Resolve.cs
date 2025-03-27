using CleanArchitecture.UseCases.Todos.Resolve;
using FastEndpoints;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.Web.Todos;

public class Resolve(IMediator mediator)
  : Endpoint<MarkTodoAsResolvedRequest>
{
    public override void Configure()
    {
        Patch(MarkTodoAsResolvedRequest.Route);
        Description(x => x.Accepts<MarkTodoAsResolvedRequest>());
        AllowAnonymous();
    }

    public override async Task HandleAsync(
      MarkTodoAsResolvedRequest req,
      CancellationToken ct)
    {
        var command = new ResolveTodoCommand(req.TodoId);

        var result = await mediator.Send(command, ct);

        await this.SendResponseAsync(result);
    }
}
