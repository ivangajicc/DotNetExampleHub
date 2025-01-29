using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Mediator;

// Message sent by an IColleague implementation
public record Message(IColleague Sender, string Content);
