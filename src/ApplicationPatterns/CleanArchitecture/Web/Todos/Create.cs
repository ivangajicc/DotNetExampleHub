using CleanArchitecture.UseCases.Todos.Create;
using FastEndpoints;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.Web.Todos;

public class Create(IMediator mediator) : Endpoint<CreateTodoRequest, CreateTodoResponse>
{
    public override void Configure()
    {
        Post(CreateTodoRequest.Route);
        AllowAnonymous();
        Summary(s => s.ExampleRequest = new CreateTodoRequest { Description = "Refactor code." });
    }

    public override async Task HandleAsync(
        CreateTodoRequest request,
        CancellationToken ct)
    {
        var result = await mediator.Send(new CreateTodoCommand(request.Description), ct);

        if (result.Succeeded)
        {
            Response = new CreateTodoResponse(new TodoRecord(result.Value.Id, result.Value.IsResolved, result.Value.Description));
        }
    }
}
