using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedKernel.Result;

public class FailedResult<T> : Result<T>
{
    public FailedResult(params OperationResultMessage[] errors)
        : base(errors)
    {
    }
}
