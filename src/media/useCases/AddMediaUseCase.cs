using SchoolManagmentSystem.Infra.Repositories;
using SchoolManagmentSystem.Models.Enums;

namespace SchoolManagmentSystem.UseCases.Medias
{
    public class AddMediaUseCase
    {
        private readonly IMediaRepository _mediaRepository;

        public AddMediaUseCase(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task ExecuteAsync(string filePath, int courseId, FileType fileType)
        {
            if (fileType == FileType.Video)
            {
                Console.WriteLine($"Queued video for processing: {filePath}");
                return;
            }

            await _mediaRepository.AddMediaAsync(filePath, courseId, fileType);
        }
    }
}
