using SchoolManagmentSystem.Models;
using Shared.Types.Repository;

public class GetUserAssignmentsByAssignmentIdUseCase
{
    private readonly IUserAssignmentRepository _repo;

    public GetUserAssignmentsByAssignmentIdUseCase(IUserAssignmentRepository repo)
    {
        _repo = repo;
    }


    public async Task<List<UserAssignment>> ExecuteAsync(int assignmentId, int userId, int pageNumber, int pageSize, bool? gradeFilter)
    {
        return await _repo.GetByAssignmentIdAsync(assignmentId, pageNumber, pageSize, gradeFilter);
    }
}
