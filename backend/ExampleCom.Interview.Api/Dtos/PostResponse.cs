namespace ExampleCom.Interview.Api.Dtos;

public record PostResponse(int Id, string Title, string AuthorName, string ImageUrl, DateTime CreatedAtUtc, IReadOnlyList<CommentResponse> Comments);
