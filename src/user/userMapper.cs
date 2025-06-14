using SchoolManagmentSystem.Models;

public static class UserMapper
{
    public static UserModel ToModel(User entity)
    {
        return new UserModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            IsApproved = entity.IsApproved,
            LastName = entity.LastName,
            Email = entity.Email,
            CreatedAt = entity.CreatedAt,
        };
    }
    public static IEnumerable<UserModel> ToModelList(IEnumerable<User> entities)
    {
        return entities.Select(ToModel).ToList();
    }
}
