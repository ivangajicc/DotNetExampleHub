using Microsoft.AspNetCore.Http;
using SharedKernel.Result;

namespace FastEndpoints;

public static class Endpoint
{
    private const string GenericErrorMessage = "Unknown error occurred.";

    public static async Task SendResponseAsync<TResponse>(
        this IEndpoint ep,
        Result<TResponse> result)
    {
        switch (result)
        {
            case SuccessfulResult<TResponse>:
                await ep.HttpContext.Response.SendAsync(result.Value);
                return;
            case NotFoundResult<TResponse>:
                await ep.HttpContext.Response.SendNotFoundAsync();
                return;
            case FailedResult<TResponse>:
                await ep.HttpContext.Response.SendAsync(result.Messages.FirstOrDefault()?.Message, DecideHttpStatusCode(result.Messages.FirstOrDefault()?.Severity));
                return;
        }

        await ep.HttpContext.Response.SendAsync(GenericErrorMessage, StatusCodes.Status500InternalServerError);
    }

    public static async Task SendResponseAsync(
        this IEndpoint ep,
        Result result)
    {
        switch (result)
        {
            case SuccessfulResult:
                await ep.HttpContext.Response.SendOkAsync();
                return;
            case NotFoundResult:
                await ep.HttpContext.Response.SendNotFoundAsync();
                return;
            case FailedResult:
                await ep.HttpContext.Response.SendAsync(result.Messages.FirstOrDefault()?.Message, DecideHttpStatusCode(result.Messages.FirstOrDefault()?.Severity));
                return;
        }

        await ep.HttpContext.Response.SendAsync(GenericErrorMessage, StatusCodes.Status500InternalServerError);
    }

    private static int DecideHttpStatusCode(OperationResultSeverity? resultSeverity) => resultSeverity switch
    {
        OperationResultSeverity.Information or OperationResultSeverity.Warning => StatusCodes.Status400BadRequest,
        OperationResultSeverity.Error => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status500InternalServerError,
    };
}
