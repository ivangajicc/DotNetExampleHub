using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MappingService.Core.Models;

namespace Utilities.MappingService.Core.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> AllAsync(CancellationToken cancellationToken);
}
