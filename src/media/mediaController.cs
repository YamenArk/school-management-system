using Auth.Jwt;
using Microsoft.AspNetCore.Mvc;

using SchoolManagmentSystem.Media.Queue;
using SchoolManagmentSystem.Models.Enums;
using SchoolManagmentSystem.Services;

namespace SchoolManagmentSystem.Features.medias
{
    [ApiController]
    [Route("medias")]
    public class MediaController : ControllerBase
    {
        private readonly MediaService _mediaService;
        private readonly IFileQueue _fileQueue;


        public MediaController(MediaService mediaService, IFileQueue fileQueue)
        {
            _mediaService = mediaService;
            _fileQueue = fileQueue;
        }
        [RolesAuthorization(UserType.Teacher)]
        [HttpPost]
        public async Task<IActionResult> AddMedia([FromForm] IFormFile file, [FromForm] int courseId, [FromForm] FileType fileType)
        {
            if (fileType == FileType.Video)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        var filePath = await SaveFileToDiskAsync(file);
                        await _fileQueue.EnqueueAsync(new VideoProcessingJob
                        {
                            FilePath = filePath,
                            CourseId = courseId
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Background video processing failed: {ex.Message}");
                    }
                });

                return Accepted(new { message = "Video uploaded successfully. Processing in background." });
            }

            return await AddNonVideoMediaAsync(file, courseId, fileType);
        }


        private async Task<IActionResult> AddNonVideoMediaAsync(IFormFile file, int courseId, FileType fileType)
        {
            var filePath = await SaveFileToDiskAsync(file);
            await _mediaService.AddMediaAsync(filePath, courseId, fileType);
            return NoContent();
        }

        private async Task<string> SaveFileToDiskAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, Guid.NewGuid() + Path.GetExtension(file.FileName));

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return filePath;
        }

        [RolesAuthorization(UserType.Teacher, UserType.Student)]
        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<List<Models.Media>>> GetMediaByCourseId(int courseId)
        {
            var mediaList = await _mediaService.GetMediaByCourseIdAsync(courseId);

            if (mediaList == null || mediaList.Count == 0)
            {
                return NotFound($"No media found for course with id {courseId}");
            }

            return Ok(mediaList);
        }
    }
}