using System.ComponentModel.DataAnnotations;

namespace ExampleCom.Interview.Api.Dtos;

public class CreateCommentRequest
{
    [Required, StringLength(1000, MinimumLength = 1)]
    public string Message { get; set; } = string.Empty;
}
