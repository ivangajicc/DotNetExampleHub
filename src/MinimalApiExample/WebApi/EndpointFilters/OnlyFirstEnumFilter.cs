using WebApi.Enums;

namespace WebApi.EndpointFilters;

public class OnlyFirstEnumFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var exampleEnumFromRequest = context.GetArgument<Example>(0);
        if (exampleEnumFromRequest != Example.ImFirst)
        {
            return TypedResults.Problem(
                detail: "Endpoint accepts only ImFirst.",
                statusCode: StatusCodes.Status400BadRequest);
        }

        return await next(context);
    }
}
