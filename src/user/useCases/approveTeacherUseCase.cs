using School.Common.Exceptions;
using Shared.Types.Repository;

public class ApproveTeacherUseCase
{
    private readonly IUserRepository _userRepository;

    public ApproveTeacherUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(int teacherId)
    {
        var user = await _userRepository.GetByIdAsync(teacherId);
        if (user == null)
            throw new UserNotFoundException();

        if (user.IsApproved)


            user.IsApproved = true;
        await _userRepository.UpdateAsync(user);
    }
}
