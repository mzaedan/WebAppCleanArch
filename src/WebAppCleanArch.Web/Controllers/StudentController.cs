using Microsoft.AspNetCore.Mvc;
using WebAppCleanArch.Application.Students;
using WebAppCleanArch.Domain.Entities;

namespace WebAppCleanArch.Web.Controllers;

public class StudentController : Controller
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: Student
    public async Task<IActionResult> Index()
    {
        var students = await _studentService.GetAllAsync();
        return View(students);
    }

    // GET: Student/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var student = await _studentService.GetDetailsAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

    // GET: Student/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Student/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (ModelState.IsValid)
        {
            await _studentService.CreateAsync(student);
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: Student/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

    // POST: Student/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await _studentService.UpdateAsync(student);
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: Student/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

    // POST: Student/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _studentService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
