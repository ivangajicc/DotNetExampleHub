using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Mapperly.Core.Models;
using Utilities.Mapperly.Core.Repositories;

namespace Utilities.Mapperly.FakeInfrastructure;

public class TodoRepository : ITodoRepository
{
    public Task<IEnumerable<Todo>> AllAsync(CancellationToken cancellationToken)
    {
        var todoList = new List<Todo>
        {
            new Todo(1, "Desc1", 1),
            new Todo(2, "Desc2", 2),
            new Todo(3, "Desc3", 3),
        };

        return Task.FromResult(todoList.AsEnumerable());
    }
}
