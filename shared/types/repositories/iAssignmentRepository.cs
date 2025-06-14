using SchoolManagmentSystem.Models;

namespace Shared.Types.Repository
{
    public interface IAssignmentepository
    {
        Task<List<Assignment>> GetAssignmentsAsync(Course course, int pageNumber, int pageSize);
        Task<Assignment?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateAssignmentDto dto, int createdByUserId, Course course);
        Task AddUserAssignmentAsync(UserAssignment userAssignment);
    }
}
