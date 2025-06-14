namespace SchoolManagmentSystem.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public required int MaxMark { get; set; }

        public required string FileUrl { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public List<UserAssignment> UserAssignments { get; set; } = new();

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;
    }
}
