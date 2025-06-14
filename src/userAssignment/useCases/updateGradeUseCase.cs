using Shared.Types.Repository;

public class UpdateGradeUseCase
{
    private readonly IUserAssignmentRepository _repo;

    public UpdateGradeUseCase(IUserAssignmentRepository repo)
    {
        _repo = repo;
    }

    public Task ExecuteAsync(int id, int grade) =>
        _repo.UpdateGradeAsync(id, grade);
}
