namespace School.Common.Exceptions
{
    public class UnauthorizedException : SchoolException
    {
        public UnauthorizedException() : base("Invalid credentials") { }
    }
}
