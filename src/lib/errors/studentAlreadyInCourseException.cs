namespace School.Common.Exceptions
{
    public class StudentAlreadyInCourseException : SchoolException
    {
        public StudentAlreadyInCourseException() : base("User is already enrolled in the course.") { }
    }
}
