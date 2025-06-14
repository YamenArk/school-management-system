using SchoolManagmentSystem.Models;
using Shared.Types.Repository;

public class GetMyUserAssignmentsUseCase
{
    private readonly IUserAssignmentRepository _repo;

    public GetMyUserAssignmentsUseCase(IUserAssignmentRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<UserAssignment>> ExecuteAsync(int userId, int pageNumber, int pageSize, bool? submitAssignmentsFilter)
    {

        return await _repo.GetMyUserAssignmentsAsync(userId, pageNumber, pageSize, submitAssignmentsFilter);
    }
}