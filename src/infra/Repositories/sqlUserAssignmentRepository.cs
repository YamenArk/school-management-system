using Microsoft.EntityFrameworkCore;

using SchoolManagmentSystem.Infra.Data;
using SchoolManagmentSystem.Models;
using Shared.Types.Dtos;
using Shared.Types.Repository;

public class UserAssignmentRepository : IUserAssignmentRepository
{
    private readonly AppDbContext _context;

    public UserAssignmentRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<UserAssignment>> GetMyUserAssignmentsAsync(int userId, int pageNumber, int pageSize, bool? submitAssignmentsFilter)
    {
        var query = _context.UserAssignments.Where(ua => ua.UserId == userId);

        if (submitAssignmentsFilter.HasValue)
        {
            query = query.Where(ua => ua.SubmitAssignments == submitAssignmentsFilter.Value);
        }

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<UserAssignment>> GetByAssignmentIdAsync(int assignmentId, int pageNumber, int pageSize, bool? gradeFilter)
    {
        var query = _context.UserAssignments
                    .Where(ua => ua.AssignmentId == assignmentId);

        if (gradeFilter.HasValue)
        {
            if (gradeFilter.Value)
            {
                query = query.Where(ua => ua.Grade.HasValue && ua.Grade.Value > 0);
            }
            else
            {
                query = query.Where(ua => !ua.Grade.HasValue || ua.Grade == 0);
            }
        }
        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task UpdateSubmitAssignmentAsync(int id, UpdateSubmitAssignmentDto dto)
    {
        var ua = await _context.UserAssignments.FindAsync(id);
        if (ua != null)
        {
            ua.SubmitAssignments = dto.SubmitAssignments;
            ua.FileUrl = dto.FileUrl;
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateGradeAsync(int id, int grade)
    {
        var ua = await _context.UserAssignments.FindAsync(id);
        if (ua != null)
        {
            ua.Grade = grade;
            await _context.SaveChangesAsync();
        }
    }
}
