using SchoolManagmentSystem.Models;

namespace Shared.Types.Repository
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task AddAdminAsync(CreateAdminDto dto, string hashedPassword);
        Task AddStudentAsync(RegisterUserDto dto, string hashedPassword);
        Task AddTeacherAsync(RegisterUserDto dto, string hashedPassword);
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAdmins(int pageNumber, int pageSize);
        Task<List<User>> GetTeachers(int pageNumber, int pageSize, bool? isApprovedFilter);
        Task<List<User>> GetStudents(int pageNumber, int pageSize);
        Task UpdateAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<string> GetUserRoleAsync(int userId);
        Task<bool> CheckIfStudentAsync(User user);
        Task<bool> CheckIfTeacherAsync(User user);
    }
}
