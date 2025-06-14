using Microsoft.EntityFrameworkCore;

using SchoolManagmentSystem.Models;
using SchoolManagmentSystem.Infra.Data;
using Shared.Types.Repository;


namespace SchoolManagmentSystem.Infra.Repositories.Assignments
{
    public class SqlAssignmentRepository : IAssignmentepository
    {
        private readonly AppDbContext _context;
        public SqlAssignmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Assignment>> GetAssignmentsAsync(Course course, int pageNumber, int pageSize)
        {

            return await _context.Assignments
            .Where(a => a.CourseId == course.Id)
            .OrderBy(a => a.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }

        public async Task<Assignment?> GetByIdAsync(int id)
        {
            return await _context.Assignments.FindAsync(id);
        }

        public async Task<int> CreateAsync(CreateAssignmentDto dto, int createdByUserId, Course course)
        {
            var assignment = new Assignment
            {
                Title = dto.Title,
                FileUrl = dto.FileUrl,
                MaxMark = dto.MaxMark,
                CreatedAt = DateTime.UtcNow,
                CourseId = course.Id,
                Course = course,
                CreatedById = createdByUserId,
            };

            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
            return assignment.Id;
        }
        public async Task AddUserAssignmentAsync(UserAssignment userAssignment)
        {
            _context.UserAssignments.Add(userAssignment);
            await _context.SaveChangesAsync();
        }
    }
}
