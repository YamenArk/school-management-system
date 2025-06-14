using SchoolManagmentSystem.Models;

public static class CourseMapper
{
    public static CourseModel ToModel(Course entity)
    {
        return new CourseModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt
        };
    }

    public static IEnumerable<CourseModel> ToModelList(IEnumerable<Course> entities)
    {
        return entities.Select(ToModel).ToList();
    }
}
