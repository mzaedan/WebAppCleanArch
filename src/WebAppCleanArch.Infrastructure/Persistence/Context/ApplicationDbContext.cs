using Microsoft.EntityFrameworkCore;
using WebAppCleanArch.Application.Common.Interfaces;
using WebAppCleanArch.Domain.Entities;

namespace WebAppCleanArch.Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Student)
            .WithMany(s => s.Courses)
            .HasForeignKey(c => c.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}