using Microsoft.AspNetCore.Identity;

using School.Common.Exceptions;
using Shared.Types.Repository;

public class RegisterStudentUseCase
{
    private readonly IUserRepository _userRepo;
    private readonly PasswordHasher<RegisterUserDto> _passwordHasher;
    public RegisterStudentUseCase(IUserRepository userRepo)
    {
        _userRepo = userRepo;
        _passwordHasher = new PasswordHasher<RegisterUserDto>();
    }

    public async Task ExecuteAsync(RegisterUserDto dto)
    {
        bool exists = await _userRepo.EmailExistsAsync(dto.Email);
        if (exists)
            throw new EmailExistException();

        string hashedPassword = HashPassword(dto);
        await _userRepo.AddStudentAsync(dto, hashedPassword);
    }

    private string HashPassword(RegisterUserDto dto)
    {
        return _passwordHasher.HashPassword(dto, dto.Password);
    }
}
