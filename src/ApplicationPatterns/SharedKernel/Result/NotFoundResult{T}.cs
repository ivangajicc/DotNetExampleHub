using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Result;

public class NotFoundResult<T> : Result<T>
{
    public NotFoundResult(params OperationResultMessage[] errors)
        : base(errors)
    {
    }
}
