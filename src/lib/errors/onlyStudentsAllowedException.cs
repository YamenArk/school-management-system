namespace School.Common.Exceptions
{
    public class OnlyStudentsAllowedException : SchoolException
    {
        public OnlyStudentsAllowedException() : base("Only students can be added to courses") { }
    }
}
