using Auth.Jwt;
using Microsoft.AspNetCore.Mvc;

using School.Common.Exceptions;

namespace SchoolManagmentSystem.Features.Users
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] AuthLoginDto dto)
        {
            try
            {
                var tokenResponse = await _userService.LoginAsync(dto);
                return Ok(tokenResponse);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed Login user - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                await _userService.RegisterUserAsync(dto);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed registering user - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }
        [HttpPost("admin")]
        [RolesAuthorization(UserType.Admin)]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminDto dto)
        {
            try
            {
                await _userService.CreateAdminAsync(dto);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed creating admin - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }

        [HttpGet("admins")]
        [RolesAuthorization(UserType.Admin)]
        public async Task<IActionResult> GetAdmins([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var admins = await _userService.GetAdminsAsync(pageNumber, pageSize);
                var dtos = UserMapper.ToModelList(admins);
                return Ok(dtos);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed getting admins - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }

        [HttpGet("teachers")]
        [RolesAuthorization(UserType.Admin)]
        public async Task<IActionResult> GetTeachers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? approved = "all")
        {
            try
            {
                bool? isApprovedFilter = approved?.ToLower() switch
                {
                    "true" => true,
                    "false" => false,
                    _ => null
                };
                var teachers = await _userService.GetTeachersAsync(pageNumber, pageSize, isApprovedFilter);
                var dtos = UserMapper.ToModelList(teachers);
                return Ok(dtos);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed getting teachers - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }

        [HttpGet("students")]
        [RolesAuthorization(UserType.Admin, UserType.Teacher)]
        public async Task<IActionResult> GetStudents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var students = await _userService.GetStudentsAsync(pageNumber, pageSize);
                var dtos = UserMapper.ToModelList(students);
                return Ok(dtos);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed getting students - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }
        [HttpPut("approve/{teacherId}")]
        [RolesAuthorization(UserType.Admin)]
        public async Task<IActionResult> ApproveTeacher(int teacherId)

        {
            try
            {
                await _userService.ApproveTeacherAsync(teacherId);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed approve teacher - {e}");
                return SchoolHttpExceptionMapper.Map(e);
            }
        }
    }
}
