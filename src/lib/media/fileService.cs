using SchoolManagmentSystem.Src.Media;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;

    public FileService(IWebHostEnvironment env)
    {
        _env = env ?? throw new ArgumentNullException(nameof(env));
    }
    public async Task<string> UploadAsync(IFormFile file)
    {
        var webRoot = _env.WebRootPath;
        if (string.IsNullOrEmpty(webRoot))
        {
            // fallback to ContentRootPath if WebRootPath is null
            webRoot = _env.ContentRootPath;
        }

        var folder = Path.Combine(webRoot, "uploads");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var path = Path.Combine(folder, fileName);

        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/uploads/{fileName}";
    }

}
