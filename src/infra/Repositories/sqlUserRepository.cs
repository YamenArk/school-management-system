using Microsoft.EntityFrameworkCore;

using SchoolManagmentSystem.Models;
using SchoolManagmentSystem.Infra.Data;
using Shared.Types.Repository;

namespace SchoolManagmentSystem.Infra.Repositories.Assignments
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public SqlUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task AddAdminAsync(CreateAdminDto dto, string hashedPassword)
        {
            var role = await GetOrCreateRoleAsync("Admin");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Email = dto.Email,
                Password = hashedPassword,
                IsApproved = true,
                CreatedAt = DateTime.UtcNow,
                Roles = new List<Role> { role }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddStudentAsync(RegisterUserDto dto, string hashedPassword)
        {
            var role = await GetOrCreateRoleAsync("Student");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Email = dto.Email,
                Password = hashedPassword,
                IsApproved = true,
                CreatedAt = DateTime.UtcNow,
                Roles = new List<Role> { role }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task AddTeacherAsync(RegisterUserDto dto, string hashedPassword)
        {
            var role = await GetOrCreateRoleAsync("Teacher");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Email = dto.Email,
                Password = hashedPassword,
                IsApproved = false,
                CreatedAt = DateTime.UtcNow,
                Roles = new List<Role> { role }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAdmins(int pageNumber, int pageSize)
        {
            return await _context.Users
                .Where(u => u.Roles.Any(r => r.RoleName == "Admin"))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<User>> GetTeachers(int pageNumber, int pageSize, bool? isApprovedFilter)
        {
            var query = _context.Users
                .Where(u => u.Roles.Any(r => r.RoleName == "Teacher"));

            if (isApprovedFilter.HasValue)
            {
                query = query.Where(u => u.IsApproved == isApprovedFilter.Value);
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<User>> GetStudents(int pageNumber, int pageSize)
        {
            return await _context.Users
                .Where(u => u.Roles.Any(r => r.RoleName == "Student"))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        private async Task<Role> GetOrCreateRoleAsync(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);

            if (role == null)
            {
                role = new Role { RoleName = roleName };
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
            }

            return role;
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<string> GetUserRoleAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.Roles?.FirstOrDefault()?.RoleName ?? "NoRole";
        }
        public async Task<bool> CheckIfStudentAsync(User user)
        {
            return await _context.Users
                .Where(u => u.Id == user.Id)
                .AnyAsync(u => u.Roles.Any(r => r.RoleName == "Student"));
        }

        public async Task<bool> CheckIfTeacherAsync(User user)
        {
            return await _context.Users
                .Where(u => u.Id == user.Id)
                .AnyAsync(u => u.Roles.Any(r => r.RoleName == "Teacher"));
        }
    }
}
