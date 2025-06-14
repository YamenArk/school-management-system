using Shared.Types.Repository;
using SchoolManagmentSystem.Models;

public class GetTeachersUseCase
{
    private readonly IUserRepository _userRepo;

    public GetTeachersUseCase(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public Task<List<User>> ExecuteAsync(int pageNumber, int pageSize, bool? isApprovedFilter)
    {
        return _userRepo.GetTeachers(pageNumber, pageSize, isApprovedFilter);
    }
}
