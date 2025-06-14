using Shared.Types.Dtos;
using SchoolManagmentSystem.Models;

namespace Shared.Types.Repository
{
    public interface IUserAssignmentRepository
    {
        Task<List<UserAssignment>> GetMyUserAssignmentsAsync(int userId, int pageNumber, int pageSize, bool? submitAssignmentsFilter);
        Task<List<UserAssignment>> GetByAssignmentIdAsync(int assignmentId, int pageNumber, int pageSize, bool? gradeFilter);
        Task UpdateSubmitAssignmentAsync(int id, UpdateSubmitAssignmentDto dto);
        Task UpdateGradeAsync(int id, int grade);

    }
}
