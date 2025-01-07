using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Result;

public static class ResultFactory
{
    public static Result Failure(params OperationResultMessage[] errors) => new FailedResult(errors);

    public static Result<T> Success<T>(T value) => new SuccessfulResult<T>(value);

    public static Result Success() => new SuccessfulResult();
}
