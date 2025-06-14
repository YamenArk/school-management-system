namespace SchoolManagmentSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public bool IsApproved { get; set; } = false;
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Role> Roles { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
        public List<UserAssignment> UserAssignments { get; set; } = new();

        public List<Course> CreatedCourses { get; set; } = new();
        public List<Assignment> CreatedAssignment { get; set; } = new();

    }
}
