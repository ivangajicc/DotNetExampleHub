using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.UseCases.Abstractions;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Todos.Resolve;

public class ResolveTodoHandler(IResolveTodoService resolveTodoService) : ICommandHandler<ResolveTodoCommand>
{
    public Task<Result> Handle(ResolveTodoCommand request, CancellationToken cancellationToken)
        => resolveTodoService.ResolveTodoAsync(request.TodoId, cancellationToken);
}
