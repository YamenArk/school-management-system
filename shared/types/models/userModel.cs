public class UserModel
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public bool IsApproved { get; set; } = false;
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}