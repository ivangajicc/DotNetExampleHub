using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.ToDoAggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Core.ToDoAggregate.Handlers;

internal class TodoResolvedEventHandler(ILogger<TodoResolvedEventHandler> logger) : INotificationHandler<TodoResolvedEvent>
{
    public Task Handle(TodoResolvedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Hey I am reacting on {EventName}. Glad to hear that ToDo with ID - {Id} was resolved.", nameof(TodoResolvedEvent), notification.TodoId);

        return Task.CompletedTask;
    }
}
