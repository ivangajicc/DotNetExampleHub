using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedKernel.Result;

public class Result<T>
{
    protected Result(params OperationResultMessage[] messages)
    {
        Succeeded = false;
        Messages = [.. messages];
    }

    protected Result(T value)
    {
        Value = value;
        Succeeded = value is not null;
    }

    protected Result(T value, params OperationResultMessage[] messages)
    {
        Value = value;
        Messages = [..messages];
        Succeeded = value is not null;
    }

    public virtual string Type => GetType().Name;

    [MemberNotNullWhen(true, nameof(Value))]
    public bool Succeeded { get; private init; }

    public T? Value { get; init; }

    public ImmutableList<OperationResultMessage> Messages { get; private init; } = [];

    // Implicit conversion from Result to Result<T>
    public static implicit operator Result<T>(Result result) => new([.. result.Messages]);

    public static Result<T> Failure(params OperationResultMessage[] errors) => new FailedResult<T>(errors);

    public static Result<T> NotFound(params OperationResultMessage[] errors) => new NotFoundResult<T>(errors);

    public static Result<T> Success(T value) => new SuccessfulResult<T>(value);
}
