using SchoolManagmentSystem.Infra.Data;
using SchoolManagmentSystem.Models;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        // app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.MapControllers();

        return app;
    }

    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var predefinedRoles = new[] { "Admin", "Teacher", "Student" };

        foreach (var roleName in predefinedRoles)
        {
            if (!context.Roles.Any(r => r.RoleName == roleName))
            {
                context.Roles.Add(new Role
                {
                    RoleName = roleName,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

        await context.SaveChangesAsync();
    }
}
