using Microsoft.EntityFrameworkCore;

using SchoolManagmentSystem.Models;
using SchoolManagmentSystem.Infra.Data;
using Shared.Types.Repository;
using Shared.Types.Dtos;

namespace SchoolManagmentSystem.Infra.Repositories.Courses
{
    public class SqlCourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public SqlCourseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Course?> GetByTitleAsync(string title)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(c => c.Title == title);
        }
        public async Task<List<Course>> GetCoursesAdminAsync(int pageNumber, int pageSize)
        {
            return await _context.Courses
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<Course>> GetCoursesTeacherAsync(int pageNumber, int pageSize, User user)
        {
            return await _context.Courses
                .Where(c => c.CreatedById == user.Id || c.Users.Any(u => u.Id == user.Id))
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<Course>> GetCoursesStudentAsync(int pageNumber, int pageSize, User user)
        {
            return await _context.Courses
                .Where(c => c.Users.Any(u => u.Id == user.Id))
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task CreateAsync(CreateCourseDto dto, int createdByUserId)
        {


            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                CreatedById = createdByUserId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, UpdateCourseDto dto)
        {
            var course = await GetByIdAsync(id);
            if (course == null) return;

            IfTitleIsAvailable(dto, course);
            IfDescriptionIsAvailable(dto, course);

            await _context.SaveChangesAsync();
        }
        private void IfTitleIsAvailable(UpdateCourseDto dto, Course course)
        {
            if (dto.Title is not null)
                course.Title = dto.Title;
        }

        private void IfDescriptionIsAvailable(UpdateCourseDto dto, Course course)
        {
            if (dto.Description is not null)
                course.Description = dto.Description;
        }

        public async Task DeleteAsync(int id)
        {
            var course = await GetByIdAsync(id);
            if (course == null) return;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsStudentInCourseAsync(int courseId, int userId)
        {

            return await _context.Set<Dictionary<string, object>>("CourseUser")
           .AnyAsync(cu =>
               EF.Property<int>(cu, "CoursesId") == courseId &&
               EF.Property<int>(cu, "UsersId") == userId);
        }
        public async Task<bool> IsTeacherInCourseAsync(int courseId, int userId, int CreatedById)
        {
            if (IsCourseCreator(CreatedById, userId))
                return true;

            return await _context.Set<Dictionary<string, object>>("CourseUser")
                .AnyAsync(cu =>
                    EF.Property<int>(cu, "CoursesId") == courseId &&
                    EF.Property<int>(cu, "UsersId") == userId);
        }

        private bool IsCourseCreator(int CreatedById, int userId)
        {
            return CreatedById == userId;
        }

        public async Task AddUserToCourseAsync(Course course, User user)
        {
            user.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
        public async Task<List<User>> GetStudentsByCourseIdAsync(int courseId)
        {
            return await _context.Courses
            .Where(c => c.Id == courseId)
            .SelectMany(c => c.Users)
            .Where(u => u.Roles.Any(r => r.RoleName == "Student"))
            .ToListAsync();
        }
    }

}
