using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Mapperly.Core.Models;

namespace Utilities.Mapperly.Core.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> AllAsync(CancellationToken cancellationToken);
}
