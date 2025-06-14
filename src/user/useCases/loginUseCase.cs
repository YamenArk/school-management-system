using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

using SchoolManagmentSystem.Models;
using Shared.Types.Repository;
using School.Common.Exceptions;
public class LoginUseCase
{
    private readonly IUserRepository _userRepo;

    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<CreateAdminDto> _passwordHasher;

    public LoginUseCase(IUserRepository userRepo, IConfiguration configuration)
    {
        _userRepo = userRepo;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<CreateAdminDto>();

    }

    public async Task<TokenResponseDto> ExecuteAsync(AuthLoginDto dto)
    {
        var user = await _userRepo.GetUserByEmailAsync(dto.Email);
        if (user == null)
            throw new UserNotFoundException();

        var verificationResult = _passwordHasher.VerifyHashedPassword(null!, user.Password, dto.Password);
        if (verificationResult != PasswordVerificationResult.Success)
        {
            throw new UnauthorizedException();
        }
        string userRole = await _userRepo.GetUserRoleAsync(user.Id);

        var token = GenerateJwtToken(user, userRole);

        return new TokenResponseDto { AccessToken = token };
    }


    private string GenerateJwtToken(User user, string role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}