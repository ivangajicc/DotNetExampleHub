using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanArchitecture.Core.ToDoAggregate;

public class ToDo(string description)
{
    public Guid Id { get; set; }

    public string Description { get; private set; } = description;

    public bool IsResolved { get; private set; }

    public void Resolve() => IsResolved = true;

    public void UnResolve() => IsResolved = false;
}
