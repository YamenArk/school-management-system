using Auth.Jwt;

using School.Common.Exceptions;
using SchoolManagmentSystem.Models;
using Shared.Types.Repository;

public class GetCoursesUseCase
{
    private readonly IUserRepository _userRepo;
    private readonly ICourseRepository _courseRepo;

    public GetCoursesUseCase(IUserRepository userRepo, ICourseRepository courseRepo)
    {
        _userRepo = userRepo;
        _courseRepo = courseRepo;
    }
    public async Task<List<Course>> ExecuteAsync(int pageNumber, int pageSize, int userId, UserType userRole)
    {
        var user = await _userRepo.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        if (userRole == UserType.Admin)
        {
            return await _courseRepo.GetCoursesAdminAsync(pageNumber, pageSize);
        }
        else if (userRole == UserType.Teacher)
        {
            return await _courseRepo.GetCoursesTeacherAsync(pageNumber, pageSize, user);
        }
        else
        {
            return await _courseRepo.GetCoursesStudentAsync(pageNumber, pageSize, user);
        }
    }
}
