using SchoolManagmentSystem.Models;

public class UserService
{
    private readonly RegisterStudentUseCase _registerStudentUseCase;
    private readonly RegisterTeacherUseCase _registerTeacherUseCase;
    private readonly CreateAdminUseCase _createAdminUseCase;
    private readonly GetAdminsUseCase _getAdminsUseCase;
    private readonly GetTeachersUseCase _getTeachersUseCase;
    private readonly GetStudentsUseCase _getStudentsUseCase;
    private readonly ApproveTeacherUseCase _approveTeacherUseCase;
    private readonly LoginUseCase _loginUseCase;

    public UserService(
        RegisterStudentUseCase registerStudentUseCase,
        RegisterTeacherUseCase registerTeacherUseCase,
        CreateAdminUseCase createAdminUseCase,
        GetAdminsUseCase getAdminsUseCase,
        GetTeachersUseCase getTeachersUseCase,
        GetStudentsUseCase getStudentsUseCase,
        LoginUseCase loginUseCase,
        ApproveTeacherUseCase approveTeacherUseCase)
    {
        _loginUseCase = loginUseCase;
        _registerStudentUseCase = registerStudentUseCase;
        _registerTeacherUseCase = registerTeacherUseCase;
        _createAdminUseCase = createAdminUseCase;
        _getAdminsUseCase = getAdminsUseCase;
        _getTeachersUseCase = getTeachersUseCase;
        _getStudentsUseCase = getStudentsUseCase;
        _approveTeacherUseCase = approveTeacherUseCase;
    }
    public Task<TokenResponseDto> LoginAsync(AuthLoginDto dto)
    {
        return _loginUseCase.ExecuteAsync(dto);
    }

    public async Task RegisterUserAsync(RegisterUserDto dto)
    {
        var role = dto.Role.ToLower();
        if (role == "student")
        {
            await _registerStudentUseCase.ExecuteAsync(dto);
        }
        else if (role == "teacher")
        {
            await _registerTeacherUseCase.ExecuteAsync(dto);
        }
    }
    public Task CreateAdminAsync(CreateAdminDto dto)
    {
        return _createAdminUseCase.ExecuteAsync(dto);
    }

    public Task<List<User>> GetAdminsAsync(int pageNumber, int pageSize)
    {
        return _getAdminsUseCase.ExecuteAsync(pageNumber, pageSize);
    }

    public Task<List<User>> GetTeachersAsync(int pageNumber, int pageSize, bool? isApprovedFilter)
    {
        return _getTeachersUseCase.ExecuteAsync(pageNumber, pageSize, isApprovedFilter);
    }

    public Task<List<User>> GetStudentsAsync(int pageNumber, int pageSize)
    {
        return _getStudentsUseCase.ExecuteAsync(pageNumber, pageSize);
    }
    public async Task ApproveTeacherAsync(int teacherId)
    {
        await _approveTeacherUseCase.ExecuteAsync(teacherId);
    }
}
