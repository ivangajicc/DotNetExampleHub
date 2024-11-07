using System.Text.Json.Serialization;
using DesignPatterns.Strategy;
using DesignPatterns.Strategy.CustomCollections;
using DesignPatterns.Strategy.CustomCollections.SortStrategy;
using DesignPatterns.Strategy.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);
builder.UseStringSerializationForEnums();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

SortableCollection<HowIShouldBeSortedIKnowMasterJoda> sortable = new(
    [
        new HowIShouldBeSortedIKnowMasterJoda() { JediLevel = 3 },
        new HowIShouldBeSortedIKnowMasterJoda() { JediLevel = 1 },
        new HowIShouldBeSortedIKnowMasterJoda() { JediLevel = 2 },
    ]);

app.MapGet("/", () => sortable.Items);

app.MapPut("/", (ReplaceSortStrategyRequestDto replaceSortStrategyRequest) =>
{
    ISortStrategy<HowIShouldBeSortedIKnowMasterJoda> strategy = replaceSortStrategyRequest.SortOrderToSet == SortOrder.Ascending
        ? new SortAscendingStrategy<HowIShouldBeSortedIKnowMasterJoda>()
        : new SortDescendingStrategy<HowIShouldBeSortedIKnowMasterJoda>();
    sortable.SetSortStrategy(strategy);
    sortable.Sort();
    return sortable.Items;
});

app.Run();
