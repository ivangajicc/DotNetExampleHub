using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedKernel.Result;

// This can be extended in application business layer to more domain driven results like - UnitOutOfOrderResult
public class FailedResult : Result
{
    public FailedResult(params OperationResultMessage[] errors)
        : base(false, errors)
    {
    }
}
