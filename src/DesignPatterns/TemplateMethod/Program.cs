using DesignPatterns.TemplateMethod;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSingleton<SearchMachine>(x => new LinearSearchMachine(1, 6, 2, 5))
    .AddSingleton<SearchMachine>(x => new BinarySearchMachine(1, 3, 5, 7, 9));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/search/{number}", SearchForIndex);

app.Run();

static IEnumerable<SearchResult> SearchForIndex(int number, IEnumerable<SearchMachine> searchMachines)
{
    foreach (var searchMachine in searchMachines)
    {
        var name = searchMachine.GetType().Name;
        var index = searchMachine.IndexOf(number);
        var found = index.HasValue;

        yield return new SearchResult(number, name, found, index);
    }
}
