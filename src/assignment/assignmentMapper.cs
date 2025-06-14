using SchoolManagmentSystem.Models;

public static class AssignmentMapper
{
    public static AssignmentModel ToModel(Assignment entity)
    {
        return new AssignmentModel
        {
            Id = entity.Id,
            Title = entity.Title,
            FileUrl = entity.FileUrl,
            MaxMark = entity.MaxMark,
            CreatedAt = entity.CreatedAt
        };
    }

    public static IEnumerable<AssignmentModel> ToModelList(IEnumerable<Assignment> entities)
    {
        return entities.Select(ToModel).ToList();
    }
}
