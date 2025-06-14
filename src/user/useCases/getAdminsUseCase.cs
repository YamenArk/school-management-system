using Shared.Types.Repository;
using SchoolManagmentSystem.Models;

public class GetAdminsUseCase
{
    private readonly IUserRepository _userRepo;

    public GetAdminsUseCase(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public Task<List<User>> ExecuteAsync(int pageNumber, int pageSize)
    {
        return _userRepo.GetAdmins(pageNumber, pageSize);
    }
}
