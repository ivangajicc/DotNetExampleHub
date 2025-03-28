using FastEndpoints;
using FluentValidation;

namespace CleanArchitecture.Web.Todos;

public class CreateTodoValidator : Validator<CreateTodoRequest>
{
    public CreateTodoValidator()
        => RuleFor(x => x.Description).MinimumLength(1).MaximumLength(256);
}
