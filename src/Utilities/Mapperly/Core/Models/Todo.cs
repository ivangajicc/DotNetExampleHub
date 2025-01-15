using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mapperly.Core.Models;

public class Todo
{
    public Todo(int id, string description, int priority)
    {
        Id = id;
        Description = description;
        Priority = priority;
    }

    public int Id { get; set; }

    public string Description { get; set; }

    public int Priority { get; set; }
}
