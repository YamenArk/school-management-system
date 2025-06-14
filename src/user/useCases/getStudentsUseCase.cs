using Shared.Types.Repository;
using SchoolManagmentSystem.Models;

public class GetStudentsUseCase
{
    private readonly IUserRepository _userRepo;

    public GetStudentsUseCase(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public Task<List<User>> ExecuteAsync(int pageNumber, int pageSize)
    {
        return _userRepo.GetStudents(pageNumber, pageSize);
    }
}
