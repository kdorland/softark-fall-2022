var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

String[] frugter = new String[]
{
    "æble", "banan", "pære", "ananas"
};

app.MapGet("/", () => "Hello World!");

/*
GET /api/fruit: Returnerer hele frugt-arrayet.
GET /api/fruit/{index}: Returnerer navnet på en bestemt frugt. Frugten findes i dit frugt-array under index, som er et tal.
GET /api/fruit/random: Returnerer navnet på en tilfældig frugt, dvs. en frugt med et tilfældigt index i arrayet.
*/

app.MapGet("/api/fruit", () => frugter);
app.MapGet("/api/fruit/{index}", (int index) => frugter[index]);
app.MapGet("/api/fruit/random", () => {
    var random = new Random();
    return frugter[random.Next(frugter.Length)];
});

app.MapPost("/api/fruit", (Fruit fruit) =>
{
    if (string.IsNullOrEmpty(fruit.name)) {
        // Return 400
        return Results.BadRequest();
    } else {
        frugter = frugter.Append(fruit.name).ToArray();
        return Results.Ok(frugter);
    }
});


app.Run();


record Fruit(string name);