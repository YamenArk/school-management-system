using School.Common.Exceptions;
using SchoolManagmentSystem.Models;
using Shared.Types.Repository;

public class GetAssignmentsUseCase
{
    private readonly IAssignmentepository _assignmentRepo;
    private readonly ICourseRepository _courseRepo;
    public GetAssignmentsUseCase(IAssignmentepository assignmentRepo, ICourseRepository courseRepo)
    {
        _assignmentRepo = assignmentRepo;
        _courseRepo = courseRepo;
    }
    public async Task<List<Assignment>> ExecuteAsync(int id, int pageNumber, int pageSize, int userId)
    {
        var course = await _courseRepo.GetByIdAsync(id);
        if (course == null)
        {
            throw new CourseNotFoundException();
        }
        bool exists = await _courseRepo.IsTeacherInCourseAsync(id, userId, course.CreatedById);
        if (!exists)
            throw new UnauthorizedException();
        return await _assignmentRepo.GetAssignmentsAsync(course, pageNumber, pageSize);
    }
}
