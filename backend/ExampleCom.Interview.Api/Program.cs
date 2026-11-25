using ExampleCom.Interview.Api.Data;
using ExampleCom.Interview.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=interview-question-009.db"));
builder.Services.AddCors(options => options.AddPolicy("VueFrontend", policy =>
    policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
          .AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UseCors("VueFrontend");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

// Create the SQLite database and seed the sample post on first launch.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
    if (!await db.Posts.AnyAsync())
    {
        db.Posts.Add(new Post
        {
            Title = "IT 08-1",
            AuthorName = "Change can",
            ImageUrl = "/images/post-puppy-kitten.png",
            CreatedAtUtc = new DateTime(2021, 10, 16, 9, 0, 0, DateTimeKind.Utc),
            Comments = new List<Comment>
            {
                new() { AuthorName = "Blend 285", Message = "have a good day", CreatedAtUtc = new DateTime(2021, 10, 16, 9, 5, 0, DateTimeKind.Utc) }
            }
        });
        await db.SaveChangesAsync();
    }
}

app.Run();
