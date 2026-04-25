using System.ComponentModel.DataAnnotations;

public class CreateNote
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
}
