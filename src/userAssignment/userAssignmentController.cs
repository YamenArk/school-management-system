using Auth.Jwt;
using Microsoft.AspNetCore.Mvc;

using Shared.Types.Dtos;

[Route("userAssignment")]
[ApiController]
public class UserAssignmentController : ControllerBase
{
    private readonly UserAssignmentService _service;

    public UserAssignmentController(UserAssignmentService service)
    {
        _service = service;
    }

    [HttpGet("my")]
    [RolesAuthorization(UserType.Student)]
    public async Task<IActionResult> GetMyUserAssignments(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? submitAssignments = "all"
    )
    {
        int userId = int.Parse(HttpContext.Items["UserId"]!.ToString()!);

        bool? submitAssignmentsFilter = null;
        if (!string.IsNullOrEmpty(submitAssignments) && submitAssignments.ToLower() != "all")
        {
            if (bool.TryParse(submitAssignments, out bool parsed))
            {
                submitAssignmentsFilter = parsed;
            }
        }

        var result = await _service.GetMyUserAssignmentsAsync(userId, pageNumber, pageSize, submitAssignmentsFilter);
        return Ok(result);
    }

    [HttpGet("assignment/{assignmentId}")]
    [RolesAuthorization(UserType.Teacher)]

    public async Task<IActionResult> GetByAssignmentId(
        int assignmentId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? gradeFilter = "all"
        )
    {
        int userId = int.Parse(HttpContext.Items["UserId"]!.ToString()!);


        bool? isGradedFilter = null;
        if (!string.IsNullOrEmpty(gradeFilter) && gradeFilter.ToLower() != "all")
        {
            if (gradeFilter.ToLower() == "graded")
                isGradedFilter = true;
            else if (gradeFilter.ToLower() == "ungraded")
                isGradedFilter = false;
        }
        var result = await _service.GetByAssignmentIdAsync(assignmentId, userId, pageNumber, pageSize, isGradedFilter);
        return Ok(result);
    }

    [HttpPut("{id}/submit")]
    [RolesAuthorization(UserType.Student)]
    public async Task<IActionResult> UpdateSubmitAssignment(
        int id,
        [FromBody] UpdateSubmitAssignmentDto dto)
    {
        await _service.UpdateSubmitAssignmentAsync(id, dto);
        return NoContent();
    }

    [HttpPut("{id}/grade")]
    [RolesAuthorization(UserType.Teacher)]
    public async Task<IActionResult> UpdateGrade(int id, [FromBody] UpdateGradeDto dto)
    {
        await _service.UpdateGradeAsync(id, dto.Grade);
        return NoContent();
    }
}
