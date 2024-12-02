using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .Configure<JsonOptions>(o
    => o.SerializerOptions.Converters.Add(
    new JsonStringEnumConverter()));

builder.Services
.AddSingleton<CommonPracticePatterns.OperationResult.SimplestForm.Executor>()
.AddSingleton<CommonPracticePatterns.OperationResult.SingleError.Executor>()
.AddSingleton<CommonPracticePatterns.OperationResult.SingleErrorWithValue.Executor>()
.AddSingleton<CommonPracticePatterns.OperationResult.MultipleErrorsWithValue.Executor>()
.AddSingleton<CommonPracticePatterns.OperationResult.WithSeverity.Executor>()
.AddSingleton<CommonPracticePatterns.OperationResult.SingleErrorWithValue.Executor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet(
"/simplest-form",
(CommonPracticePatterns.OperationResult.SimplestForm.Executor executor) =>
{
    var result = executor.Operation();
    if (result.Succeeded)
    {
        // Handle the success
        return "Operation succeeded";
    }
    else
    {
        // Handle the failure
        return "Operation failed";
    }
});

app.MapGet(
"/single-error",
(CommonPracticePatterns.OperationResult.SingleError.Executor executor) =>
{
    var result = executor.Operation();
    if (result.Succeeded)
    {
        // Handle the success
        return "Operation succeeded";
    }
    else
    {
        // Handle the failure
        return result.ErrorMessage;
    }
});

app.MapGet(
"/single-error-with-value",
(CommonPracticePatterns.OperationResult.SingleErrorWithValue.Executor executor) =>
{
    var result = executor.Operation();
    if (result.Succeeded)
    {
        // Handle the success
        return $"Operation succeeded with a value of '{result.Value}'.";
    }
    else
    {
        // Handle the failure
        return result.ErrorMessage;
    }
});

app.MapGet(
"/multiple-errors-with-value",
Results<Ok<string>, BadRequest<string[]>> (CommonPracticePatterns.OperationResult.MultipleErrorsWithValue.Executor executor)
=>
{
    var result = executor.Operation();

    if (result.Succeeded)
    {
        // Handle the success
        return TypedResults.Ok(
        $"Operation succeeded with a value of '{result.Value}'.");
    }
    else
    {
        // Handle the failure
        return TypedResults.BadRequest(result.Errors.ToArray());
    }
});

app.MapGet(
    "/multiple-errors-with-value-and-severity",
    (CommonPracticePatterns.OperationResult.WithSeverity.Executor executor) =>
    {
        var result = executor.Operation();
        if (result.Succeeded)
        {
            // Handle the success
        }
        else
        {
            // Handle the failure
        }

        return result;
});

app.MapGet("/final-version-with-static-factory-methods", (CommonPracticePatterns.OperationResult.FinalVersionWithStaticFactory.Executor executor) =>
{
    var result = executor.Operation();
    if (result.Succeeded)
    {
        // Handle the success
    }
    else
    {
        // Handle the failure
    }

// 1. We can decide on success to return just result value and on failure to wrap whole result in bad request.
// e.g. we can have Match extension on our result

// 2.we can use this technique when sending the result to another system over HTTP (like this
// project does) or publish the operation result as an event when using microservices
    return result;
});

app.Run();
