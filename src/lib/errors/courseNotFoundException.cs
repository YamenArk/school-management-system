namespace School.Common.Exceptions
{
    public class CourseNotFoundException : SchoolException
    {
        public CourseNotFoundException() : base("Course not found") { }
    }
}
