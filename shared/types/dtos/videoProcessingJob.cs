namespace SchoolManagmentSystem.Media.Queue
{
    public class VideoProcessingJob
    {
        public string FilePath { get; set; } = default!;
        public int CourseId { get; set; }
    }
}
