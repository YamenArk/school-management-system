namespace School.Common.Exceptions
{
    public class DuplicateCourseTitleException : SchoolException
    {
        public DuplicateCourseTitleException() : base("Course with this title already exists") { }
    }
}
