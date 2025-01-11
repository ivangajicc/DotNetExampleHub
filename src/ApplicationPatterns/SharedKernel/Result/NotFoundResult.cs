using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Result;

public class NotFoundResult : Result
{
    public NotFoundResult(params OperationResultMessage[] errors)
        : base(false, errors)
    {
    }
}
