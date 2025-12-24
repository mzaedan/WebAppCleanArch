using Microsoft.EntityFrameworkCore;
using WebAppCleanArch.Domain.Entities;

namespace WebAppCleanArch.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}