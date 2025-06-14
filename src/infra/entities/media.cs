using SchoolManagmentSystem.Models.Enums;

namespace SchoolManagmentSystem.Models
{
    public class Media
    {
        public int Id { get; set; }

        public required string Url { get; set; }

        public FileType FileType { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
