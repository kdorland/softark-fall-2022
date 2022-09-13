var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Task[] tasks = new Task[] {
    new Task { Id = 1, Text = "Hejsa 1", Done = false},
    new Task { Id = 2, Text = "Hejsa 2", Done = false},
    new Task { Id = 3, Text = "Hejsa 3", Done = true},
};

/*
GET /api/tasks
GET /api/tasks/{id}
PUT /api/tasks/{id}
DELETE /api/tasks/{id}
POST /api/tasks/
*/

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/tasks/", () => tasks);

app.MapGet("/api/tasks/{id}", (int id) => {
    return Results.Ok(tasks.Where(t => t.Id == id));
});

app.MapDelete("/api/tasks/{id}", (int id) =>
{
    tasks = tasks.Where(t => t.Id != id).ToArray();
    return Results.Ok(tasks);
});

app.Run();

public class Task {
    public int Id { get; set; }
    public string Text { get; set; }
    public bool Done { get; set; }
}
