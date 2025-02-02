using FluentValidation;
using MediatR;
using SharedKernel.Result;
using VerticalSliceArchitecture.Application.Abstractions;

namespace VerticalSliceArchitecture.Application.Common;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest>? _validator = validator;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
            .ConvertAll(error => new OperationResultMessage(error.ErrorMessage, OperationResultSeverity.Error));

        return (dynamic)Result.Failure([.. errors]);
    }
}

