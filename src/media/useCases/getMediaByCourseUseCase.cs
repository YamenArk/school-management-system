using SchoolManagmentSystem.Infra.Repositories;

namespace SchoolManagmentSystem.UseCases.Medias
{
    public class GetMediaByCourseUseCase
    {
        private readonly IMediaRepository _mediaRepository;

        public GetMediaByCourseUseCase(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<List<Models.Media>> ExecuteAsync(int courseId)
        {
            return await _mediaRepository.GetMediaByCourseIdAsync(courseId);
        }
    }
}
