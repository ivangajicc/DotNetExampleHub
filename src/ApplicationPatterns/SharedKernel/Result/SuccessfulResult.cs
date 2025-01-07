using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedKernel.Result;

#pragma warning disable SA1402 // File may only contain a single type
public class SuccessfulResult<T> : Result<T>
{
    internal SuccessfulResult(T value)
        : base(value)
    {
    }
}

public class SuccessfulResult : Result
{
    internal SuccessfulResult(params OperationResultMessage[] messages)
        : base(true, messages)
    {
    }
}
