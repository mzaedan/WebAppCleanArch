using WebAppCleanArch.Application.Students;
using WebAppCleanArch.Domain.Entities;
using WebAppCleanArch.Domain.Interfaces;

namespace WebAppCleanArch.Application.Courses;

public class CourseService
{
    private readonly ICourseRepository _repository;
    private readonly StudentService _studentService;

    public CourseService(ICourseRepository repository, StudentService studentService)
    {
        _repository = repository;
        _studentService = studentService;
    }

    public Task<List<Course>> GetAllAsync()
        => _repository.GetAllAsync();

    public Task<Course?> GetByIdAsync(int id)
        => _repository.GetByIdAsync(id);

    public Task<Course?> GetDetailsAsync(int id)
        => _repository.GetWithStudentAsync(id);

    public Task CreateAsync(Course course)
        => _repository.AddAsync(course);

    public async Task UpdateAsync(Course course)
    {
        if (!await _repository.ExistsAsync(course.Id))
            throw new KeyNotFoundException("Course not found");

        await _repository.UpdateAsync(course);
    }

    public Task DeleteAsync(int id)
        => _repository.DeleteAsync(id);

    public Task<List<Domain.Entities.Student>> GetAllStudentsAsync()
        => _studentService.GetAllAsync();
}