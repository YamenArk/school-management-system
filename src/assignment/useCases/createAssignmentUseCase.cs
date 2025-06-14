using School.Common.Exceptions;
using SchoolManagmentSystem.Models;
using Shared.Types.Repository;

public class CreateAssignmentUseCase
{
    private readonly IAssignmentepository _assignmentRepo;
    private readonly ICourseRepository _courseRepo;
    public CreateAssignmentUseCase(IAssignmentepository assignmentRepo, ICourseRepository courseRepo)
    {
        _assignmentRepo = assignmentRepo;
        _courseRepo = courseRepo;
    }
    public async Task ExecuteAsync(CreateAssignmentDto dto, int createdByUserId, int courseId)
    {
        var course = await _courseRepo.GetByIdAsync(courseId);
        if (course == null)
        {
            throw new CourseNotFoundException();
        }

        bool exists = await _courseRepo.IsTeacherInCourseAsync(course.Id, createdByUserId, course.CreatedById);
        if (!exists)
        {
            throw new UnauthorizedException();
        }
        int assignmentId = await _assignmentRepo.CreateAsync(dto, createdByUserId, course);
        var students = await _courseRepo.GetStudentsByCourseIdAsync(courseId);

        foreach (var student in students)
        {
            var userAssignment = new UserAssignment
            {
                AssignmentId = assignmentId,
                UserId = student.Id,
                SubmitAssignments = false,
                Grade = null,
                FileUrl = null,
                CreatedAt = DateTime.UtcNow
            };

            await _assignmentRepo.AddUserAssignmentAsync(userAssignment);
        }

    }
}
