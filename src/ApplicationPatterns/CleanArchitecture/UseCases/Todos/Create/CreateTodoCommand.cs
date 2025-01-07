using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Todos.Create;

public record CreateTodoCommand(string Description) : IRequest<Result<TodoDto>>;
