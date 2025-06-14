namespace SchoolManagmentSystem.Media.Queue
{
    public interface IFileQueue
    {
        Task EnqueueAsync(VideoProcessingJob job);
    }
}
