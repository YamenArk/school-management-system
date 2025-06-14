namespace SchoolManagmentSystem.Models
{
    public class UserAssignment
    {
        public int Id { get; set; }
        public bool SubmitAssignments { get; set; }
        public int? Grade { get; set; }

        public string? FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
