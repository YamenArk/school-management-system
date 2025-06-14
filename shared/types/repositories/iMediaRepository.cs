using SchoolManagmentSystem.Models.Enums;

namespace SchoolManagmentSystem.Infra.Repositories
{
    public interface IMediaRepository
    {
        Task AddMediaAsync(string url, int courseId, FileType fileType);
        Task<List<Models.Media>> GetMediaByCourseIdAsync(int courseId);
    }
}
