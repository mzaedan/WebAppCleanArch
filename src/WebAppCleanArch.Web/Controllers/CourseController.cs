using Microsoft.AspNetCore.Mvc;
using WebAppCleanArch.Application.Courses;
using WebAppCleanArch.Application.Students;
using WebAppCleanArch.Domain.Entities;

namespace WebAppCleanArch.Web.Controllers;

public class CourseController : Controller
{
    private readonly CourseService _courseService;
    private readonly StudentService _studentService;

    public CourseController(CourseService courseService, StudentService studentService)
    {
        _courseService = courseService;
        _studentService = studentService;
    }

    // GET: Course
    public async Task<IActionResult> Index()
    {
        var courses = await _courseService.GetAllAsync();
        return View(courses);
    }

    // GET: Course/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var course = await _courseService.GetDetailsAsync(id);
        if (course == null) return NotFound();
        return View(course);
    }

    // GET: Course/Create
    public async Task<IActionResult> Create()
    {
        var students = await _studentService.GetAllAsync();
        ViewBag.Students = students;
        return View();
    }

    // POST: Course/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Course course)
    {
        if (ModelState.IsValid)
        {
            await _courseService.CreateAsync(course);
            return RedirectToAction(nameof(Index));
        }
        
        // Repopulate ViewBag.Students for dropdown when validation fails
        var students = await _studentService.GetAllAsync();
        ViewBag.Students = students;
        
        return View(course);
    }

    // GET: Course/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null) return NotFound();
        
        var students = await _studentService.GetAllAsync();
        ViewBag.Students = students;
        
        return View(course);
    }

    // POST: Course/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Course course)
    {
        if (id != course.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await _courseService.UpdateAsync(course);
            return RedirectToAction(nameof(Index));
        }
        
        // Repopulate ViewBag.Students for dropdown when validation fails
        var students = await _studentService.GetAllAsync();
        ViewBag.Students = students;
        
        return View(course);
    }

    // GET: Course/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null) return NotFound();
        return View(course);
    }

    // POST: Course/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _courseService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}