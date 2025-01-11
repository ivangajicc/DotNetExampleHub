using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedKernel.Result;

public class Result
{
    protected Result(bool succeeded, params OperationResultMessage[] messages)
    {
        Messages = [.. messages];
        Succeeded = succeeded;
    }

    private Result()
    {
    }

    public virtual string Type => GetType().Name;

    public bool Succeeded { get; private init; } = true;

    public ImmutableList<OperationResultMessage> Messages { get; private init; } = [];

    public static Result Failure(params OperationResultMessage[] errors) => new FailedResult(errors);

    public static Result NotFound(params OperationResultMessage[] errors) => new NotFoundResult(errors);

    public static Result Success() => new SuccessfulResult();
}
