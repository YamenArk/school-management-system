namespace Auth.Jwt
{
    public class JwtPayload
    {
        public int Id { get; set; }
        public UserType Type { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
