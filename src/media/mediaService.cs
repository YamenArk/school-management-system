using SchoolManagmentSystem.Models.Enums;
using SchoolManagmentSystem.UseCases.Medias;


namespace SchoolManagmentSystem.Services
{
    public class MediaService
    {
        private readonly AddMediaUseCase _addMediaUseCase;
        private readonly GetMediaByCourseUseCase _getMediaByCourseUseCase;

        public MediaService(
            AddMediaUseCase addMediaUseCase,
            GetMediaByCourseUseCase getMediaByCourseUseCase)
        {
            _addMediaUseCase = addMediaUseCase;
            _getMediaByCourseUseCase = getMediaByCourseUseCase;
        }

        public async Task AddMediaAsync(string filePath, int courseId, FileType fileType)
        {
            await _addMediaUseCase.ExecuteAsync(filePath, courseId, fileType);
        }

        public async Task<List<Models.Media>> GetMediaByCourseIdAsync(int courseId)
        {
            return await _getMediaByCourseUseCase.ExecuteAsync(courseId);
        }
    }
}
