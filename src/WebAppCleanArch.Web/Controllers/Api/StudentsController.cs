using Microsoft.AspNetCore.Mvc;
using WebAppCleanArch.Application.Students;
using WebAppCleanArch.Domain.Entities;

namespace WebAppCleanArch.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentsController(StudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(students);
    }

    // GET: api/students/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _studentService.GetByIdAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    // GET: api/students/5/details
    [HttpGet("{id}/details")]
    public async Task<ActionResult<Student>> GetStudentDetails(int id)
    {
        var student = await _studentService.GetDetailsAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        await _studentService.CreateAsync(student);
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    // PUT: api/students/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, Student student)
    {
        if (id != student.Id)
        {
            return BadRequest();
        }

        try
        {
            await _studentService.UpdateAsync(student);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        await _studentService.DeleteAsync(id);
        return NoContent();
    }
}
