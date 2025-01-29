using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Mediator;

// Entity that can send messages to colleagues.
public interface IMediator
{
    void Send(Message message);
}
