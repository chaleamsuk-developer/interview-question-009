using ExampleCom.Interview.Api.Data;
using ExampleCom.Interview.Api.Dtos;
using ExampleCom.Interview.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleCom.Interview.Api.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController(AppDbContext db) : ControllerBase
{
    private const string CommenterName = "Blend 285";

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostResponse>> GetPost(int id)
    {
        var post = await db.Posts.AsNoTracking()
            .Include(x => x.Comments.OrderBy(c => c.CreatedAtUtc))
            .FirstOrDefaultAsync(x => x.Id == id);

        if (post is null) return NotFound(new { message = "Post not found." });
        return Ok(ToResponse(post));
    }

    [HttpPost("{id:int}/comments")]
    public async Task<ActionResult<CommentResponse>> AddComment(int id, [FromBody] CreateCommentRequest request)
    {
        var postExists = await db.Posts.AnyAsync(x => x.Id == id);
        if (!postExists) return NotFound(new { message = "Post not found." });

        var message = request.Message.Trim();
        if (string.IsNullOrWhiteSpace(message))
            return BadRequest(new { message = "Comment cannot be empty." });

        var comment = new Comment
        {
            PostId = id,
            AuthorName = CommenterName,
            Message = message,
            CreatedAtUtc = DateTime.UtcNow
        };

        db.Comments.Add(comment);
        await db.SaveChangesAsync();

        var response = new CommentResponse(comment.Id, comment.AuthorName, comment.Message, comment.CreatedAtUtc);
        return CreatedAtAction(nameof(GetPost), new { id }, response);
    }

    private static PostResponse ToResponse(Post post) => new(
        post.Id, post.Title, post.AuthorName, post.ImageUrl, post.CreatedAtUtc,
        post.Comments.Select(c => new CommentResponse(c.Id, c.AuthorName, c.Message, c.CreatedAtUtc)).ToList());
}
