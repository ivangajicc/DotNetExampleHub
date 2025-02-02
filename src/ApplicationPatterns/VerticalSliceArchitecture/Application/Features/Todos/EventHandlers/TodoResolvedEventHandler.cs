using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VerticalSliceArchitecture.Application.Domain.Todos.Events;

namespace VerticalSliceArchitecture.Application.Features.Todos.EventHandlers;

internal class TodoResolvedEventHandler(ILogger<TodoResolvedEventHandler> logger) : INotificationHandler<TodoResolvedEvent>
{
    public Task Handle(TodoResolvedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Hey I am reacting on {EventName}. Glad to hear that ToDo with ID - {Id} was resolved.", nameof(TodoResolvedEvent), notification.TodoId);

        return Task.CompletedTask;
    }
}
