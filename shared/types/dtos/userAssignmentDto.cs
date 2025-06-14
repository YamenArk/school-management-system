namespace Shared.Types.Dtos;

public class UserAssignmentDto
{
    public int Id { get; set; }
    public int Grade { get; set; }
    public bool SubmitAssignments { get; set; }
    public string? FileUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class UpdateSubmitAssignmentDto
{
    public bool SubmitAssignments { get; set; } = true;
    public string? FileUrl { get; set; }
}
