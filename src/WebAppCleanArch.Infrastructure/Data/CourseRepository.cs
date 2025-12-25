using Microsoft.EntityFrameworkCore;
using WebAppCleanArch.Domain.Entities;
using WebAppCleanArch.Domain.Interfaces;
using WebAppCleanArch.Infrastructure.Persistence.Context;

namespace WebAppCleanArch.Infrastructure.Data;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Course>> GetAllAsync()
        => _context.Courses.ToListAsync();

    public Task<Course?> GetByIdAsync(int id)
        => _context.Courses.FindAsync(id).AsTask();

    public Task<Course?> GetWithStudentAsync(int id)
        => _context.Courses
            .Include(c => c.Student)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(Course course)
    {
        _context.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Course course)
    {
        _context.Update(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }

    public Task<bool> ExistsAsync(int id)
        => _context.Courses.AnyAsync(e => e.Id == id);
}