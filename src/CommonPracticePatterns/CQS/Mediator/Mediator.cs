using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPracticePatterns.CQS.Mediator;

public class Mediator : IMediator
{
    private readonly HandlerDictionary _handlers = new();

    public void Register<TCommand>(ICommandHandler<TCommand> commandHandler)
        where TCommand : ICommand => _handlers.AddHandler(commandHandler);

    public void Register<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> commandHandler)
        where TQuery : IQuery<TReturn> => _handlers.AddHandler(commandHandler);

    public TReturn Send<TQuery, TReturn>(TQuery query)
        where TQuery : IQuery<TReturn>
    {
        var handler = _handlers.Find<TQuery, TReturn>();
        return handler.Handle(query);
    }

    public void Send<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        var handlers = _handlers.FindAll<TCommand>();
        foreach (var handler in handlers)
        {
            handler.Handle(command);
        }
    }

    private sealed class HandlerDictionary
    {
        private readonly Dictionary<Type, HandlerList> _handlers = new();

        public void AddHandler<TCommand>(ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            var type = typeof(TCommand);
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            registeredHandlers.Add(handler);
        }

        public void AddHandler<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> handler)
            where TQuery : IQuery<TReturn>
        {
            var type = typeof(TQuery);
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            registeredHandlers.Add(handler);
        }

        public IEnumerable<ICommandHandler<TCommand>> FindAll<TCommand>()
            where TCommand : ICommand
        {
            var type = typeof(TCommand);
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            return registeredHandlers.FindAll<TCommand>();
        }

        public IQueryHandler<TQuery, TReturn> Find<TQuery, TReturn>()
            where TQuery : IQuery<TReturn>
        {
            var type = typeof(TQuery);
            EnforceTypeEntry(type);
            var registeredHandlers = _handlers[type];
            return registeredHandlers.Find<TQuery, TReturn>();
        }

        private void EnforceTypeEntry(Type type)
        {
            if (!_handlers.ContainsKey(type))
            {
                _handlers.Add(type, new HandlerList());
            }
        }
    }

    private class HandlerList
    {
        private readonly List<object> _commandHandlers = new();
        private readonly List<object> _queryHandlers = new();

        public void Add<TCommand>(ICommandHandler<TCommand> handler)
            where TCommand : ICommand => _commandHandlers.Add(handler);

        public void Add<TQuery, TReturn>(IQueryHandler<TQuery, TReturn> handler)
            where TQuery : IQuery<TReturn> => _queryHandlers.Add(handler);

        public IEnumerable<ICommandHandler<TCommand>> FindAll<TCommand>()
            where TCommand : ICommand
        {
            foreach (var handler in _commandHandlers)
            {
                if (handler is ICommandHandler<TCommand> output)
                {
                    yield return output;
                }
            }
        }

        public IQueryHandler<TQuery, TReturn> Find<TQuery, TReturn>()
            where TQuery : IQuery<TReturn>
        {
            foreach (var handler in _queryHandlers)
            {
                if (handler is IQueryHandler<TQuery, TReturn> output)
                {
                    return output;
                }
            }

            throw new QueryHandlerNotFoundException(typeof(TQuery));
        }
    }
}

public class QueryHandlerNotFoundException : Exception
{
    public QueryHandlerNotFoundException(Type queryType)
        : base($"No handler found for query '{queryType}'.")
    {
    }
}
