using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Any;
using WebApi.Enums;

namespace WebApi.Extensions;

public static class BasicExamplesEndpoints
{
    public static void MapBasicExamplesEndpoints(this IEndpointRouteBuilder app)
    {
        MapBasicMetadataExample(app);
        MapBasicJsonSerializationConfigurationExample(app);
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

    private static void MapBasicMetadataExample(IEndpointRouteBuilder app)
    {
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
}
