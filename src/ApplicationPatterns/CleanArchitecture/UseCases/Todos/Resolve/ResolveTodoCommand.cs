using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.UseCases.Abstractions;
using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Todos.Resolve;

public record ResolveTodoCommand(Guid TodoId) : ICommand;
