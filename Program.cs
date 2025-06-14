// using SchoolManagmentSystem.Infra.Data; // Your AppDbContext namespace
// using Shared.Types.Repository;           // Your ICourseRepository interface namespace
// using Microsoft.EntityFrameworkCore;
// using SchoolManagmentSystem.Infra.Repositories.Courses;
// using FluentValidation.AspNetCore;
// using FluentValidation;
// using SchoolManagmentSystem.Infra.Repositories.Assignments;
// using SchoolManagmentSystem.Models;
// using SchoolManagmentSystem.Media.Queue;
// using System.Threading.Channels;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;
// using Auth.Jwt;
// using SchoolManagmentSystem.Src.Media;

// var builder = WebApplication.CreateBuilder(args); // <-- Only once here!

// builder.Services.AddScoped<LoginUseCase>();

// // JWT Authentication Setup (only using Jwt:Key)
// var jwtKey = builder.Configuration["Jwt:Key"];
// if (string.IsNullOrWhiteSpace(jwtKey))
// {
//     throw new InvalidOperationException("JWT key is not configured.");
// }

// var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
//     ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));





// // Register repository implementation
// builder.Services.AddScoped<ICourseRepository, SqlCourseRepository>();
// builder.Services.AddScoped<IAssignmentepository, SqlAssignmentRepository>();

// // Register Use Cases
// builder.Services.AddScoped<CreateAssignmentUseCase>();
// builder.Services.AddScoped<GetAssignmentsUseCase>();

// builder.Services.AddControllers();
// builder.Services.AddJwtAuth(builder.Configuration);

// // Register Service Layer
// builder.Services.AddScoped<AssignmentService>();

// // Register use cases
// builder.Services.AddScoped<CreateCourseUseCase>();
// builder.Services.AddScoped<DeleteCourseUseCase>();
// builder.Services.AddScoped<GetCoursesUseCase>();
// builder.Services.AddScoped<UpdateCourseUseCase>();

// builder.Services.AddFluentValidationAutoValidation();
// builder.Services.AddFluentValidationClientsideAdapters();
// builder.Services.AddValidatorsFromAssemblyContaining<CreateCourseDtoValidator>();

// // Register the service that uses the use cases
// builder.Services.AddScoped<CourseService>();

// // user assignment
// builder.Services.AddScoped<UserAssignmentService>();

// // Use Cases
// builder.Services.AddScoped<GetMyUserAssignmentsUseCase>();
// builder.Services.AddScoped<GetUserAssignmentsByAssignmentIdUseCase>();
// builder.Services.AddScoped<UpdateGradeUseCase>();
// builder.Services.AddScoped<UpdateSubmitAssignmentUseCase>();

// // Infra / Repository
// builder.Services.AddScoped<IUserAssignmentRepository, UserAssignmentRepository>();

// // users
// builder.Services.AddScoped<UserService>();
// builder.Services.AddScoped<RegisterStudentUseCase>();
// builder.Services.AddScoped<RegisterTeacherUseCase>();
// builder.Services.AddScoped<CreateAdminUseCase>();
// builder.Services.AddScoped<GetAdminsUseCase>();
// builder.Services.AddScoped<GetTeachersUseCase>();
// builder.Services.AddScoped<GetStudentsUseCase>();
// builder.Services.AddScoped<AddStudentToCourseUseCase>();
// builder.Services.AddScoped<AddTeacherToCourseUseCase>();
// builder.Services.AddScoped<ApproveTeacherUseCase>();


// // Register your IFileService with its implementation FileService
// builder.Services.AddScoped<IFileService, FileService>();


// // media 
// builder.Services.AddSingleton(Channel.CreateUnbounded<VideoProcessingJob>());
// // builder.Services.AddSingleton<IFileQueue, FileQueue>();
// // builder.Services.AddHostedService<VideoFileProcessor>();

// // Also don't forget to register IUserRepository and its implementation:
// builder.Services.AddScoped<IUserRepository, SqlUserRepository>();

// // Controllers
// builder.Services.AddControllers();

// // Swagger (optional)
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
// app.UseAuthentication();
// app.UseAuthorization();

// // app.UseHttpsRedirection();
// // Enable static files middleware
// app.UseStaticFiles();

// app.MapControllers();

// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//     var predefinedRoles = new[] { "Admin", "Teacher", "Student" };

//     foreach (var roleName in predefinedRoles)
//     {
//         if (!context.Roles.Any(r => r.RoleName == roleName))
//         {
//             context.Roles.Add(new Role
//             {
//                 RoleName = roleName,
//                 CreatedAt = DateTime.UtcNow
//             });
//         }
//     }

//     await context.SaveChangesAsync();
// }

// app.Run();
// // Program.cs
// // using SchoolManagmentSystem.Extensions;

// // var builder = WebApplication.CreateBuilder(args);

// // builder.Services.ConfigureAll(builder.Configuration);

// // var app = builder.Build();

// // app.UseApp(); // Your custom extension (auth, swagger, etc.)
// // await app.SeedDatabaseAsync(); // Your DB seeding
// // app.Run();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

app.ConfigurePipeline();

await app.InitializeDatabaseAsync();

app.Run();
