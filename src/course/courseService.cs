using Auth.Jwt;

using SchoolManagmentSystem.Models;
using Shared.Types.Dtos;
public class CourseService
{
    private readonly CreateCourseUseCase _createCourseUseCase;
    private readonly DeleteCourseUseCase _deleteCourseUseCase;
    private readonly GetCoursesUseCase _getCoursesUseCase;
    private readonly UpdateCourseUseCase _updateCourseUseCase;
    private readonly AddStudentToCourseUseCase _addStudentToCourseUseCase;
    private readonly AddTeacherToCourseUseCase _addTeacherToCourseUseCase;

    public CourseService(
        CreateCourseUseCase createCourseUseCase,
        DeleteCourseUseCase deleteCourseUseCase,
        GetCoursesUseCase getCoursesUseCase,
        UpdateCourseUseCase updateCourseUseCase,
        AddStudentToCourseUseCase addStudentToCourseUseCase,
        AddTeacherToCourseUseCase addTeacherToCourseUseCase)
    {
        _createCourseUseCase = createCourseUseCase;
        _deleteCourseUseCase = deleteCourseUseCase;
        _getCoursesUseCase = getCoursesUseCase;
        _updateCourseUseCase = updateCourseUseCase;
        _addStudentToCourseUseCase = addStudentToCourseUseCase;
        _addTeacherToCourseUseCase = addTeacherToCourseUseCase;
    }

    public Task CreateCourseAsync(CreateCourseDto dto, int createdByUserId)
    {
        return _createCourseUseCase.ExecuteAsync(dto, createdByUserId);
    }

    public Task DeleteCourseAsync(int id, int userId, UserType userRole)
    {
        return _deleteCourseUseCase.ExecuteAsync(id, userId, userRole);
    }

    public async Task<IEnumerable<Course>> GetCoursesAsync(int pageNumber, int pageSize, int userId, UserType userRole)
    {
        return await _getCoursesUseCase.ExecuteAsync(pageNumber, pageSize, userId, userRole);
    }

    public Task UpdateCourseAsync(int id, UpdateCourseDto dto, int userId, UserType userRole)
    {
        return _updateCourseUseCase.ExecuteAsync(id, dto, userId, userRole);
    }
    public Task AddStudentToCourseAsync(int courseId, int userId)
    {
        return _addStudentToCourseUseCase.ExecuteAsync(courseId, userId);
    }
    public Task AddTeacherToCourseAsync(int courseId, int userId)
    {
        return _addTeacherToCourseUseCase.ExecuteAsync(courseId, userId);
    }
}
