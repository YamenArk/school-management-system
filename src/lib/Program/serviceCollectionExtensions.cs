using System.Threading.Channels;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Auth.Jwt;
using FluentValidation;
using FluentValidation.AspNetCore;

using SchoolManagmentSystem.Infra.Data;
using Shared.Types.Repository;
using SchoolManagmentSystem.Infra.Repositories.Courses;
using SchoolManagmentSystem.Infra.Repositories.Assignments;
using SchoolManagmentSystem.Media.Queue;
using SchoolManagmentSystem.Src.Media;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Your existing registrations, exactly as is

        services.AddScoped<LoginUseCase>();

        var jwtKey = configuration["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException("JWT key is not configured.");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // Repository
        services.AddScoped<ICourseRepository, SqlCourseRepository>();
        services.AddScoped<IAssignmentepository, SqlAssignmentRepository>();

        // Use cases
        services.AddScoped<CreateAssignmentUseCase>();
        services.AddScoped<GetAssignmentsUseCase>();

        services.AddControllers();
        services.AddJwtAuth(configuration);

        // Service Layer
        services.AddScoped<AssignmentService>();

        services.AddScoped<CreateCourseUseCase>();
        services.AddScoped<DeleteCourseUseCase>();
        services.AddScoped<GetCoursesUseCase>();
        services.AddScoped<UpdateCourseUseCase>();

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<CreateCourseDtoValidator>();

        services.AddScoped<CourseService>();

        // User assignment
        services.AddScoped<UserAssignmentService>();

        services.AddScoped<GetMyUserAssignmentsUseCase>();
        services.AddScoped<GetUserAssignmentsByAssignmentIdUseCase>();
        services.AddScoped<UpdateGradeUseCase>();
        services.AddScoped<UpdateSubmitAssignmentUseCase>();

        // Infra
        services.AddScoped<IUserAssignmentRepository, UserAssignmentRepository>();

        // Users
        services.AddScoped<UserService>();
        services.AddScoped<RegisterStudentUseCase>();
        services.AddScoped<RegisterTeacherUseCase>();
        services.AddScoped<CreateAdminUseCase>();
        services.AddScoped<GetAdminsUseCase>();
        services.AddScoped<GetTeachersUseCase>();
        services.AddScoped<GetStudentsUseCase>();
        services.AddScoped<AddStudentToCourseUseCase>();
        services.AddScoped<AddTeacherToCourseUseCase>();
        services.AddScoped<ApproveTeacherUseCase>();

        services.AddScoped<IFileService, FileService>();

        // Media queue
        services.AddSingleton(Channel.CreateUnbounded<VideoProcessingJob>());
        // services.AddSingleton<IFileQueue, FileQueue>();
        // services.AddHostedService<VideoFileProcessor>();

        services.AddScoped<IUserRepository, SqlUserRepository>();

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
