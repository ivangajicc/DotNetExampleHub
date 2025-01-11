using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.UseCases.Abstractions;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Todos.GetById;

public record GetTodoByIdQuery(Guid Id) : IQuery<TodoDto>;
