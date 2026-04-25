using System.ComponentModel.DataAnnotations;

public class UpdateNote
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
}
