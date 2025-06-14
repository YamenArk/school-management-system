namespace Shared.Types.Dtos
{
    public class CreateCourseDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }

    public class UpdateCourseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
