using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Result;

namespace CleanArchitecture.Core.Interfaces;

public interface IResolveTodoService
{
    public Task<Result> ResolveTodoAsync(Guid todoId, CancellationToken cancellationToken);
}
