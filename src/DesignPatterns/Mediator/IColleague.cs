using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Mediator;

// Entity that can receive messages.
public interface IColleague
{
    string Name { get; }

    void ReceiveMessage(Message message);
}
