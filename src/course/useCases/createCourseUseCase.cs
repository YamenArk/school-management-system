using School.Common.Exceptions;
using Shared.Types.Dtos;
using Shared.Types.Repository;

public class CreateCourseUseCase
{
    private readonly ICourseRepository _repo;

    public CreateCourseUseCase(ICourseRepository repo)
    {
        _repo = repo;
    }
    public async Task ExecuteAsync(CreateCourseDto dto, int createdByUserId)
    {
        var course = await _repo.GetByTitleAsync(dto.Title);
        if (course != null)
        {
            throw new DuplicateCourseTitleException();
        }
        await _repo.CreateAsync(dto, createdByUserId);
    }
}
