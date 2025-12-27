using Microsoft.AspNetCore.Mvc;
using WebAppCleanArch.Application.Courses;
using WebAppCleanArch.Domain.Entities;

namespace WebAppCleanArch.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly CourseService _courseService;

    public CoursesController(CourseService courseService)
    {
        _courseService = courseService;
    }

    // GET: api/courses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        var courses = await _courseService.GetAllAsync();
        return Ok(courses);
    }

    // GET: api/courses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(int id)
    {
        var course = await _courseService.GetByIdAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    // GET: api/courses/5/details
    [HttpGet("{id}/details")]
    public async Task<ActionResult<Course>> GetCourseDetails(int id)
    {
        var course = await _courseService.GetDetailsAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    // POST: api/courses
    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse(Course course)
    {
        await _courseService.CreateAsync(course);
        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }

    // PUT: api/courses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, Course course)
    {
        if (id != course.Id)
        {
            return BadRequest();
        }

        try
        {
            await _courseService.UpdateAsync(course);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/courses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        await _courseService.DeleteAsync(id);
        return NoContent();
    }
}
