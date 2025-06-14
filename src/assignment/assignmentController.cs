using Auth.Jwt;
using Microsoft.AspNetCore.Mvc;

using School.Common.Exceptions;
using SchoolManagmentSystem.Src.Media;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("assignments")]
    public class AssignmentController : ControllerBase
    {
        private readonly AssignmentService _assignmentService;
        private readonly IFileService _iFileService;
        private readonly ILogger<AssignmentController> _logger;
        public AssignmentController(AssignmentService assignmentService, ILogger<AssignmentController> logger, IFileService iFileService)
        {
            _iFileService = iFileService;
            _assignmentService = assignmentService;
            _logger = logger;
        }

        [HttpGet("course/{courseId:int}")]
        [RolesAuthorization(UserType.Teacher)]
        public async Task<ActionResult> GetAssignments(int courseId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                int userId = int.Parse(HttpContext.Items["UserId"]!.ToString()!);
                var entities = await _assignmentService.GetAssignmentsAsync(courseId, pageNumber, pageSize, userId);
                var dtos = AssignmentMapper.ToModelList(entities);
                return Ok(dtos);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed getting assignments - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }

        [HttpPost("course/{courseId:int}")]
        [RolesAuthorization(UserType.Teacher)]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> CreateAssignment(int courseId, [FromForm] CreateAssignmentFormDto dto)
        {
            try
            {
                if (!dto.File.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    return BadRequest("Only PDF files are allowed.");

                var fileUrl = await _iFileService.UploadAsync(dto.File);
                int userId = int.Parse(HttpContext.Items["UserId"]!.ToString()!);
                var assignmentDto = new CreateAssignmentDto
                {
                    Title = dto.Title,
                    MaxMark = dto.MaxMark,
                    FileUrl = fileUrl
                };

                await _assignmentService.CreateAssignmentAsync(assignmentDto, userId, courseId);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed creating assignment - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }
    }
}