using Shared.Types.Repository;
using School.Common.Exceptions;

public class AddStudentToCourseUseCase
{
    private readonly IUserRepository _userRepo;
    private readonly ICourseRepository _courseRepo;

    public AddStudentToCourseUseCase(IUserRepository userRepo, ICourseRepository courseRepo)
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
        bool isStudent = await _userRepo.CheckIfStudentAsync(user);
        if (!isStudent)
        {
            throw new OnlyStudentsAllowedException();
        }
        bool exists = await _courseRepo.IsStudentInCourseAsync(course.Id, user.Id);
        if (exists)
            throw new StudentAlreadyInCourseException();
        await _courseRepo.AddUserToCourseAsync(course, user);
    }
}
