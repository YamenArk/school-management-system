using System.Threading.Channels;

namespace SchoolManagmentSystem.Media.Queue
{
    public class FileQueue : IFileQueue
    {
        private readonly Channel<VideoProcessingJob> _queue;

        public FileQueue(Channel<VideoProcessingJob> queue)
        {
            _queue = queue;
        }

        public Task EnqueueAsync(VideoProcessingJob job)
        {
            return _queue.Writer.WriteAsync(job).AsTask();
        }
    }
}
