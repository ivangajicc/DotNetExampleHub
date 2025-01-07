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

    public static implicit operator Result<T>(Result result) => new(default!)
    {
        Succeeded = false,
        Messages = [..result.Messages],
    };
}
