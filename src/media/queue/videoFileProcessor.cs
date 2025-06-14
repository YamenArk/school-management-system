using System.Threading.Channels;

using SchoolManagmentSystem.Models.Enums;
using SchoolManagmentSystem.Services;

namespace SchoolManagmentSystem.Media.Queue
{
    public class VideoFileProcessor : BackgroundService
    {
        private readonly Channel<VideoProcessingJob> _queue;
        private readonly MediaService _mediaService;
        private readonly ILogger<VideoFileProcessor> _logger;

        public VideoFileProcessor(Channel<VideoProcessingJob> queue, MediaService mediaService, ILogger<VideoFileProcessor> logger)
        {
            _queue = queue;
            _mediaService = mediaService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var job in _queue.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    _logger.LogInformation($"Processing video: {job.FilePath}");
                    await _mediaService.AddMediaAsync(job.FilePath, job.CourseId, FileType.Video);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing video");
                }
            }
        }
    }
}
