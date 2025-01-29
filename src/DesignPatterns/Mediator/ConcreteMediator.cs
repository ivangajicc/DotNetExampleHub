using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Mediator;

public class ConcreteMediator : IMediator
{
    private readonly List<IColleague> _colleagueList;

    public ConcreteMediator(params IColleague[] colleagues)
    {
        ArgumentNullException.ThrowIfNull(colleagues);

        _colleagueList = [..colleagues];
    }

    public void Send(Message message)
    {
        foreach (var colleague in _colleagueList)
        {
            colleague.ReceiveMessage(message);
        }
    }
}
