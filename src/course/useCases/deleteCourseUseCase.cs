using Auth.Jwt;

using School.Common.Exceptions;
using Shared.Types.Repository;

public class DeleteCourseUseCase
{
    private readonly ICourseRepository _repo;
    public DeleteCourseUseCase(ICourseRepository repo)
    {
        _repo = repo;
    }
    public async Task ExecuteAsync(int id, int userId, UserType userRole)
    {
        var course = await _repo.GetByIdAsync(id);
        if (course == null)
        {
            throw new CourseNotFoundException();
        }
        if (userRole == UserType.Teacher)
        {
            bool exists = await _repo.IsTeacherInCourseAsync(id, userId, course.CreatedById);
            if (!exists)
                throw new UnauthorizedException();

        }
        await _repo.DeleteAsync(id);
    }
}
