using Microsoft.EntityFrameworkCore;

using SchoolManagmentSystem.Models.Enums;
using SchoolManagmentSystem.Infra.Data;

namespace SchoolManagmentSystem.Infra.Repositories
{
    public class SqlMediaRepository : IMediaRepository
    {
        private readonly AppDbContext _context;

        public SqlMediaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddMediaAsync(string url, int courseId, FileType fileType)
        {
            var media = new Models.Media
            {
                Url = url,
                CourseId = courseId,
                FileType = fileType
            };

            _context.Medias.Add(media);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Models.Media>> GetMediaByCourseIdAsync(int courseId)
        {
            return await _context.Medias
                .Where(m => m.CourseId == courseId)
                .ToListAsync();
        }
    }
}
