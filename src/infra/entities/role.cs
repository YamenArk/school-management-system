namespace SchoolManagmentSystem.Models
{
    public class Role
    {
        public int Id { get; set; }
        public required string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<User> Users { get; set; } = new();

    }
}
