namespace School.Common.Exceptions
{
    public class OnlyTeachersAllowedException : SchoolException
    {
        public OnlyTeachersAllowedException() : base("Only teachers can be added to courses") { }
    }

}
