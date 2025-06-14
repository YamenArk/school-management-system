using Microsoft.AspNetCore.Identity;

using School.Common.Exceptions;
using Shared.Types.Repository;

public class CreateAdminUseCase
{
    private readonly IUserRepository _userRepo;
    private readonly PasswordHasher<CreateAdminDto> _passwordHasher;

    public CreateAdminUseCase(IUserRepository userRepo)
    {
        _userRepo = userRepo;
        _passwordHasher = new PasswordHasher<CreateAdminDto>();
    }

    public async Task ExecuteAsync(CreateAdminDto dto)
    {
        bool exists = await _userRepo.EmailExistsAsync(dto.Email);
        if (exists)
            throw new EmailExistException();

        string hashedPassword = HashPassword(dto);

        await _userRepo.AddAdminAsync(dto, hashedPassword);
    }

    private string HashPassword(CreateAdminDto dto)
    {
        return _passwordHasher.HashPassword(dto, dto.Password);
    }
}
