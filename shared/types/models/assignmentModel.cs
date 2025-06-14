public class AssignmentModel
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string FileUrl { get; set; }
    public required int MaxMark { get; set; }
    public DateTime CreatedAt { get; set; }
}
