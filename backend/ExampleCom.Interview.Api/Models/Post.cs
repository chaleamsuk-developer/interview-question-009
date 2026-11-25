namespace ExampleCom.Interview.Api.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public List<Comment> Comments { get; set; } = new();
}
