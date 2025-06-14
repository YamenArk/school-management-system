using System.ComponentModel.DataAnnotations;

public class CreateAssignmentFormDto
{
    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public int MaxMark { get; set; }

    [Required]
    public IFormFile File { get; set; } = default!;
}

public class CreateAssignmentDto
{
    public string Title { get; set; } = default!;
    public int MaxMark { get; set; }
    public string FileUrl { get; set; } = default!;
}
