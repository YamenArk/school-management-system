using Microsoft.EntityFrameworkCore;

using SchoolManagmentSystem.Models;

namespace SchoolManagmentSystem.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<UserAssignment> UserAssignments { get; set; }
        public DbSet<Models.Media> Medias { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>()
            .HasOne(c => c.CreatedBy)
            .WithMany(u => u.CreatedCourses)
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
