using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCleanArch.Domain.Entities;

public class Course
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string CourseName { get; set; }

    // FK
    public int StudentId { get; set; }

    // Navigation
    [ForeignKey(nameof(StudentId))]
    public Student Student { get; set; }
}
