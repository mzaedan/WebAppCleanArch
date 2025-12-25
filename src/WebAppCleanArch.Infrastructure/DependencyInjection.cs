using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppCleanArch.Application.Common.Interfaces;
using WebAppCleanArch.Application.Students;
using WebAppCleanArch.Application.Courses;
using WebAppCleanArch.Domain.Interfaces;
using WebAppCleanArch.Infrastructure.Data;
using WebAppCleanArch.Infrastructure.Persistence.Context;

namespace WebAppCleanArch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration?.GetConnectionString("DefaultConnection"));
        });
        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<StudentService>();
        services.AddScoped<CourseService>();
        
        return services;
    }
}
