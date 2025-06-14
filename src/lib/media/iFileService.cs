namespace SchoolManagmentSystem.Src.Media
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
