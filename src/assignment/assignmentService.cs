using SchoolManagmentSystem.Models;
public class AssignmentService
{
    private readonly CreateAssignmentUseCase _createAssignmentUseCase;
    private readonly GetAssignmentsUseCase _getAssignmentsUseCase;

    public AssignmentService(
        CreateAssignmentUseCase createAssignmentUseCase,
        GetAssignmentsUseCase getAssignmentsUseCase)
    {
        _createAssignmentUseCase = createAssignmentUseCase;
        _getAssignmentsUseCase = getAssignmentsUseCase;
    }

    public Task CreateAssignmentAsync(CreateAssignmentDto dto, int createdByUserId, int courseId)
    {
        return _createAssignmentUseCase.ExecuteAsync(dto, createdByUserId, courseId);
    }

    public async Task<IEnumerable<Assignment>> GetAssignmentsAsync(int courseId, int pageNumber, int pageSize, int userId)
    {
        return await _getAssignmentsUseCase.ExecuteAsync(courseId, pageNumber, pageSize, userId);
    }
}
