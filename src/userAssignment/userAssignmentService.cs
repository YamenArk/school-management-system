using SchoolManagmentSystem.Models;
using Shared.Types.Dtos;

public class UserAssignmentService
{
    private readonly GetMyUserAssignmentsUseCase _getMine;
    private readonly GetUserAssignmentsByAssignmentIdUseCase _getByAssignment;
    private readonly UpdateSubmitAssignmentUseCase _updateSubmit;
    private readonly UpdateGradeUseCase _updateGrade;

    public UserAssignmentService(
        GetMyUserAssignmentsUseCase getMine,
        GetUserAssignmentsByAssignmentIdUseCase getByAssignment,
        UpdateSubmitAssignmentUseCase updateSubmit,
        UpdateGradeUseCase updateGrade)
    {
        _getMine = getMine;
        _getByAssignment = getByAssignment;
        _updateSubmit = updateSubmit;
        _updateGrade = updateGrade;
    }

    public Task<List<UserAssignment>> GetMyUserAssignmentsAsync(int userId, int pageNumber, int pageSize, bool? submitAssignmentsFilter) =>
        _getMine.ExecuteAsync(userId, pageNumber, pageSize, submitAssignmentsFilter);

    public Task<List<UserAssignment>> GetByAssignmentIdAsync(int assignmentId, int userId, int pageNumber, int pageSize, bool? gradeFilter) =>
        _getByAssignment.ExecuteAsync(assignmentId, userId, pageNumber, pageSize, gradeFilter);

    public Task UpdateSubmitAssignmentAsync(int id, UpdateSubmitAssignmentDto dto) =>
        _updateSubmit.ExecuteAsync(id, dto);

    public Task UpdateGradeAsync(int id, int grade) =>
        _updateGrade.ExecuteAsync(id, grade);
}
