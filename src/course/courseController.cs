using Microsoft.AspNetCore.Mvc;
using Auth.Jwt;

using Shared.Types.Dtos;
using School.Common.Exceptions;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("courses")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;
        private readonly ILogger<CourseController> _logger;

        public CourseController(CourseService courseService, ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet]
        [RolesAuthorization(UserType.Admin, UserType.Teacher, UserType.Student)]
        public async Task<ActionResult> GetCourses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                int userId = int.Parse(HttpContext.Items["UserId"]!.ToString()!);
                UserType userRole = (UserType)HttpContext.Items["UserRole"]!;

                var entities = await _courseService.GetCoursesAsync(pageNumber, pageSize, userId, userRole);
                var dtos = CourseMapper.ToModelList(entities);
                return Ok(dtos);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed getting courses - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }

        [HttpPost]
        [RolesAuthorization(UserType.Admin, UserType.Teacher)]
        public async Task<ActionResult> CreateCourse([FromBody] CreateCourseDto dto)
        {
            try
            {
                int userId = int.Parse(HttpContext.Items["UserId"]!.ToString()!);

                await _courseService.CreateCourseAsync(dto, userId);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed creating course - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }

        [HttpPut("{id:int}")]
        [RolesAuthorization(UserType.Admin, UserType.Teacher)]
        public async Task<ActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto dto)
        {
            try
            {
                int userId = int.Parse(HttpContext.Items["UserId"]!.ToString()!);
                UserType userRole = (UserType)HttpContext.Items["UserRole"]!;
                await _courseService.UpdateCourseAsync(id, dto, userId, userRole);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed updating course {id} - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }

        [HttpDelete("{id:int}")]
        [RolesAuthorization(UserType.Admin, UserType.Teacher)]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {
                int userId = int.Parse(HttpContext.Items["UserId"]!.ToString()!);
                UserType userRole = (UserType)HttpContext.Items["UserRole"]!;
                await _courseService.DeleteCourseAsync(id, userId, userRole);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed deleting course {id} - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }

        }

        [HttpPost("{courseId:int}/students/{userId:int}")]
        [RolesAuthorization(UserType.Admin)]
        public async Task<IActionResult> AddStudentToCourse(int courseId, int userId)
        {
            try
            {
                await _courseService.AddStudentToCourseAsync(courseId, userId);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add student {userId} to course {courseId} - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }

        [HttpPost("{courseId:int}/teachers/{userId:int}")]
        [RolesAuthorization(UserType.Admin)]
        public async Task<IActionResult> AddTeacherToCourse(int courseId, int userId)
        {
            try
            {
                await _courseService.AddTeacherToCourseAsync(courseId, userId);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add teacher {userId} to course {courseId} - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }
    }
}
