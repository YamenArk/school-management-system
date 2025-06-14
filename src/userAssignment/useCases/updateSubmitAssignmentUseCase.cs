using Shared.Types.Dtos;
using Shared.Types.Repository;

public class UpdateSubmitAssignmentUseCase
{
    private readonly IUserAssignmentRepository _repo;

    public UpdateSubmitAssignmentUseCase(IUserAssignmentRepository repo)
    {
        _repo = repo;
    }

    public Task ExecuteAsync(int id, UpdateSubmitAssignmentDto dto) =>
        _repo.UpdateSubmitAssignmentAsync(id, dto);
}
