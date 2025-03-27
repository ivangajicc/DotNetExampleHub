using CleanArchitecture.UseCases.Todos.List;
using FastEndpoints;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.Web.Todos;

public class List(IMediator mediator) : EndpointWithoutRequest<ListTodoResponse>
{
    public override void Configure()
    {
        Get("/Todos");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await mediator.Send(new ListTodosQuery(null, null), ct);

        await this.SendResponseAsync(result);
    }
}
