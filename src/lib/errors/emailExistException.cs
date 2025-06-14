namespace School.Common.Exceptions
{
    public class EmailExistException : SchoolException
    {
        public EmailExistException() : base("Email already exists") { }
    }
}
