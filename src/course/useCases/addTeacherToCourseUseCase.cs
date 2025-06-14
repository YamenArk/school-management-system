using Shared.Types.Repository;
using School.Common.Exceptions;
public class AddTeacherToCourseUseCase
{
    private readonly IUserRepository _userRepo;
    private readonly ICourseRepository _courseRepo;

    public AddTeacherToCourseUseCase(IUserRepository userRepo, ICourseRepository courseRepo)
    {
        _userRepo = userRepo;
        _courseRepo = courseRepo;
    }
    public async Task ExecuteAsync(int courseId, int userId)
    {
        var course = await _courseRepo.GetByIdAsync(courseId);
        if (course == null)
        {
            throw new CourseNotFoundException();
        }
        var user = await _userRepo.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        bool isTeacher = await _userRepo.CheckIfTeacherAsync(user);
        if (!isTeacher)
        {
            throw new OnlyTeachersAllowedException();
        }
        bool exists = await _courseRepo.IsTeacherInCourseAsync(course.Id, user.Id, course.CreatedById);
        if (exists)
            throw new StudentAlreadyInCourseException();

        await _courseRepo.AddUserToCourseAsync(course, user);
    }
}
