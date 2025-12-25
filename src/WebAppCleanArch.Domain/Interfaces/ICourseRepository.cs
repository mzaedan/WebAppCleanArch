using WebAppCleanArch.Domain.Entities;

namespace WebAppCleanArch.Domain.Interfaces;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task<Course?> GetWithStudentAsync(int id);
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}