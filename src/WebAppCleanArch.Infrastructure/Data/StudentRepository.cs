using Microsoft.EntityFrameworkCore;
using WebAppCleanArch.Domain.Entities;
using WebAppCleanArch.Domain.Interfaces;
using WebAppCleanArch.Infrastructure.Persistence.Context;

namespace WebAppCleanArch.Infrastructure.Data;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Student>> GetAllAsync()
        => _context.Students.ToListAsync();

    public Task<Student?> GetByIdAsync(int id)
        => _context.Students.FindAsync(id).AsTask();

    public Task<Student?> GetWithCoursesAsync(int id)
        => _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task AddAsync(Student student)
    {
        _context.Add(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Student student)
    {
        _context.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }

    public Task<bool> ExistsAsync(int id)
        => _context.Students.AnyAsync(e => e.Id == id);
}
