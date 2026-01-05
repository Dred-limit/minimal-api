using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var _fruit = new ConcurrentDictionary<string, Fruit>();


app.MapGet("/fruit", () => _fruit);

app.MapGet("/fruit/{id}", (string id) =>
    _fruit.TryGetValue(id, out var fruit)
    ? TypedResults.Ok(fruit)
    : Results.Problem(statusCode: 404))
    .AddEndpointFilter<IdValidationFilter>();

// именованный маршрут и LinkGenerator
app.MapGet("/fruit/{id}", (string id) => $"The details is {id}")
   .WithName("GetFruitById");


app.MapPost("/fruit/{id}", (string id, Fruit fruit, LinkGenerator links) =>
    _fruit.TryAdd(id, fruit)
    ? TypedResults.Created( 
        links.GetPathByName("GetFruitById", new { id }), fruit)
    : Results.ValidationProblem(new Dictionary<string, string[]>
    {
        { "id", new[] {"A fruit with this id already exists"}}
    }));

//? TypedResults.Created($"/fruit/{id}", fruit)


app.MapPut("/fruit/{id}", (string id, Fruit fruit) =>
{
    _fruit[id] = fruit;
    return Results.NoContent();
});

app.MapDelete("/fruit/{id}", (string id) =>
{
    _fruit.TryRemove(id, out _);
    return Results.NoContent();
});
app.Run();
record Fruit(string Name, int Stock);

class IdValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var id = context.GetArgument<string>(0);
        if (string.IsNullOrEmpty(id) || !id.StartsWith('f'))
        {
            return Results.ValidationProblem(
                new Dictionary<string, string[]>
                {
                    {"id", new[]{"Invalid format. Id must start with 'f"}}
                });
        }
        return await next(context);
    }
}







