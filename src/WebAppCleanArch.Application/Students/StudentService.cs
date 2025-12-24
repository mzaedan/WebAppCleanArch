using WebAppCleanArch.Domain.Entities;
using WebAppCleanArch.Domain.Interfaces;

namespace WebAppCleanArch.Application.Students;

public class StudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Student>> GetAllAsync()
        => _repository.GetAllAsync();

    public Task<Student?> GetByIdAsync(int id)
        => _repository.GetByIdAsync(id);

    public Task<Student?> GetDetailsAsync(int id)
        => _repository.GetWithCoursesAsync(id);

    public Task CreateAsync(Student student)
        => _repository.AddAsync(student);

    public async Task UpdateAsync(Student student)
    {
        if (!await _repository.ExistsAsync(student.Id))
            throw new KeyNotFoundException("Student not found");

        await _repository.UpdateAsync(student);
    }

    public Task DeleteAsync(int id)
        => _repository.DeleteAsync(id);
}