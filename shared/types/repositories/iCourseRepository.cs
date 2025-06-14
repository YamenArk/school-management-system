using SchoolManagmentSystem.Models;
using Shared.Types.Dtos;

namespace Shared.Types.Repository
{
    public interface ICourseRepository
    {
        Task<Course?> GetByTitleAsync(string title);
        Task<List<Course>> GetCoursesAdminAsync(int pageNumber, int pageSize);
        Task<List<Course>> GetCoursesTeacherAsync(int pageNumber, int pageSize, User user);
        Task<List<Course>> GetCoursesStudentAsync(int pageNumber, int pageSize, User user);
        Task<Course?> GetByIdAsync(int id);
        Task CreateAsync(CreateCourseDto dto, int createdByUserId);
        Task UpdateAsync(int id, UpdateCourseDto dto);
        Task DeleteAsync(int id);
        Task<bool> IsStudentInCourseAsync(int courseId, int userId);
        Task<bool> IsTeacherInCourseAsync(int courseId, int userId, int CreatedById);
        Task AddUserToCourseAsync(Course course, User user);
        Task<List<User>> GetStudentsByCourseIdAsync(int courseId);
    }
}
