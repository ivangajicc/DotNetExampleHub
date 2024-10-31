using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Any;
using WebApi.EndpointFilters;
using WebApi.Enums;

namespace WebApi.Extensions;

public static class BasicExamplesEndpoints
{
    public static void MapBasicExamplesEndpoints(this IEndpointRouteBuilder app)
    {
        MapBasicMetadataExample(app);
        MapBasicJsonSerializationConfigurationExample(app);
        MapEndpointsWithFilterExample(app);
    }

    private static void MapBasicMetadataExample(IEndpointRouteBuilder app)
    {
        // MapGroup(someString) will add someString as part of base url (e.g. localhost:5200/someString)
        // WithTags(someTag) will affect swagger to group all endpoints together where someTag will reflect as grouping label.
        var endpointMetadataGroup =
            app.MapGroup("basic-examples-metadata").WithTags("Basic Examples Endpoints").WithOpenApi();

        const string someName = "someName";

        endpointMetadataGroup.MapGet("with-name", () => $"Endpoint with name {someName}.")
            .WithName(someName)
            .WithOpenApi(
                op =>
                {
                    op.Description = "And endpoint that returns its name.";
                    op.Summary = $"Endpoint named {someName}.";
                    op.Deprecated = true;
                    return op;
                });

        endpointMetadataGroup.MapGet(
                "url-of-named-endpoint/{endpointName?}",
                (string? endpointName, LinkGenerator linkGenerator) =>
                {
                    var name = endpointName ?? someName;
                    return new { name, uri = linkGenerator.GetPathByName(name), };
                })
            .WithDescription("Return the URL of the specified named endpoint.")
            .WithOpenApi(
                operation =>
                {
                    var endpointName = operation.Parameters[0];
                    endpointName.Description = "The name of the endpoint to get the URL for.";
                    endpointName.AllowEmptyValue = true;
                    endpointName.Example = new OpenApiString(someName);
                    return operation;
                });
    }

    private static void MapBasicJsonSerializationConfigurationExample(IEndpointRouteBuilder app)
    {
        var enumSerializer = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        enumSerializer.Converters.Add(new JsonStringEnumConverter());

        var jsonGroup = app.MapGroup("json-serialization").WithTags("Serializer Endpoints");
        jsonGroup.MapGet(
            "enum-as-string/",
            () => TypedResults.Json(
                new { Property1 = "Value1", Property2 = "Value2", EnumProperty = Example.ImFirst, },
                enumSerializer)).WithOpenApi();
        jsonGroup.MapGet(
            "enum-as-int/",
            () => TypedResults.Json(
                new { Property1 = "Value1", Property2 = "Value2", EnumProperty = Example.ImFirst, }));
    }

    private static void MapEndpointsWithFilterExample(IEndpointRouteBuilder app)
    {
        var groupWithInlineFilters = app.MapGroup("inline-filters-example").WithTags("Endpoints With Inline Filters");

        var sharedLoggerFactory = app.ServiceProvider
            .GetRequiredService<ILoggerFactory>();
        var logger = sharedLoggerFactory
            .CreateLogger("shared-endpoints-logger");

        groupWithInlineFilters
            .MapGet("basic-logging", () => { })
            .AddEndpointFilter(
                (context, next) =>
                {
                    logger.LogInformation("Entering basic logging example");
                    var result = next(context);
                    logger.LogInformation("Exiting basic logging example");
                    return result;
                });

        groupWithInlineFilters.MapGet(
            "only-first",
            Results<Ok<Example>, BadRequest> (Example exampleEnum) => TypedResults.Ok(exampleEnum))
            .AddEndpointFilter(// Only related to this endpoint
                async (context, next) =>
                {
                    var exampleEnumFromRequest = context.GetArgument<Example>(0);
                    if (exampleEnumFromRequest != Example.ImFirst)
                    {
                        return TypedResults.Problem(
                            detail: "Endpoint accepts only ImFirst.",
                            statusCode: StatusCodes.Status400BadRequest);
                    }

                    return await next(context);
                });

        var groupWithClassFilters = app.MapGroup("class-filters-example")
            .WithTags("Endpoints With Class Filters")
            .AddEndpointFilter<OnlyFirstEnumFilter>(); // Related to all endpoints of the group.

        groupWithClassFilters.MapGet(
            "only-first",
            Results<Ok<Example>, BadRequest> (Example exampleEnum) => TypedResults.Ok(exampleEnum));

        groupWithInlineFilters
            .MapGet("endpoint-filter-factory", () => "RAW")
            .AddEndpointFilterFactory((filterFactoryContext, next) =>
            {
                // Building RequestDelegate code here.
                var logger = filterFactoryContext.ApplicationServices
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger("endpoint-filter-factory");
                logger.LogInformation("Code that runs when ASP.NET Core builds the RequestDelegate");

                // Returns the EndpointFilterDelegate ASP.NET Core executes as part of the pipeline.
                return async invocationContext =>
                {
                    logger.LogInformation("Code that ASP.NET Core executes as part of the pipeline");

                    /* Filter code here */
                    var filter = new OnlyFirstEnumFilter();
                    return await filter.InvokeAsync(invocationContext, next);
                };
            });
    }
}
