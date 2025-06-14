namespace School.Common.Exceptions
{
    public class UserNotFoundException : SchoolException
    {
        public UserNotFoundException() : base("User not found") { }
    }
}
