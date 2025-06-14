namespace SchoolManagmentSystem.Models
{
    public class Course
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Assignment> Assignments { get; set; } = new();
        public List<User> Users { get; set; } = new();
        public List<Media> Media { get; set; } = new();

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;
    }
}
