namespace ExampleCom.Interview.Api.Dtos;

public record CommentResponse(int Id, string AuthorName, string Message, DateTime CreatedAtUtc);
