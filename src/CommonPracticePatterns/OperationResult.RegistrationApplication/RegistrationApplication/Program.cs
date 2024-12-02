using System.Text.Json.Serialization;
using CommonPracticePatterns.OperationResult.RegistrationApplication.Entities;
using CommonPracticePatterns.OperationResult.RegistrationApplication.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Json;
using static CommonPracticePatterns.OperationResult.RegistrationApplication.Results.RegistrationServiceResult;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ConcertRegistrationService>();
builder.Services.Configure<JsonOptions>(o => o.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost(
    "/concerts/{concertId}/register",
    async Task<Results<Ok<ConcertRegistrationResult>, BadRequest<ConcertRegistrationResult>>> (int concertId, ConcertRegistrationService service) =>
    {
        // Simulate fetching objects
        var user = GetCurrentUser();
        var concert = GetConcert(concertId);

        // Execute the operation
        var result = await service.RegisterAsync(user, concert);

        // Handle the operation result
        if (result.RegistrationSucceeded)
        {
            return TypedResults.Ok(result);
        }
        else
        {
            await LogErrorMessageAsync(result.ErrorMessage); // Showcases the usefulness of the MemberNotNullWhen attributes. Even if error message is not nullable compiler knows it wont be null based on condition.
            return TypedResults.BadRequest(result);
        }
    });

app.Run();

static Concert GetConcert(int concertId) => new(concertId, $"Some amazing concert—Part {concertId}");
static User GetCurrentUser() => new("John Doe");

[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1172:Unused method parameters should be removed", Justification = "Added for example purposes.")]
static Task LogErrorMessageAsync(string message) => Task.CompletedTask;
