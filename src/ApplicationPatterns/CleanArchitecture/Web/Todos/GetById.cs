using CleanArchitecture.UseCases.Todos;
using CleanArchitecture.UseCases.Todos.GetById;
using FastEndpoints;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.Web.Todos;

public class GetById(IMediator mediator) : Endpoint<GetTodoByIdRequest, TodoRecord>
{
    public override void Configure()
    {
        Get(GetTodoByIdRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTodoByIdRequest req, CancellationToken ct)
    {
        var request = new GetTodoByIdQuery(req.TodoId);

        var result = await mediator.Send(request, ct);

        await this.SendResponseAsync(result);
    }
}
