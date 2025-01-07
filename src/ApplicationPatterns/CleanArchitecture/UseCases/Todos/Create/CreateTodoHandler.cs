using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Todos.Create;

public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, Result<TodoDto>>
{
    public async Task<Result<TodoDto>> Handle(CreateTodoCommand request, CancellationToken cancellationToken) => ResultFactory.Failure();
}
