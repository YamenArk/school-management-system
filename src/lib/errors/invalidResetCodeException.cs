namespace School.Common.Exceptions
{
    public class InvalidResetCodeException : SchoolException
    {
        public InvalidResetCodeException() : base("Invalid reset code for the provided email") { }
    }
}
