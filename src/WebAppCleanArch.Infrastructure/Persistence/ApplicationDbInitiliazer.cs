using WebAppCleanArch.Domain.Seeds;
using WebAppCleanArch.Infrastructure.Persistence.Context;

namespace WebAppCleanArch.Infrastructure.Persistence;

public class ApplicationDbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        var initializer = new ApplicationDbInitializer();
        initializer.SeedAsync(context).GetAwaiter().GetResult();
    }

    private async Task SeedAsync(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        await SeedStudentAsync(context);
    }

    private async Task SeedStudentAsync(ApplicationDbContext context)
    {
        var students = StudentSeed.Students();

        foreach (var item in students)
        {
            var student = await context.Students.FindAsync(item.Id);
            if (student == null)
            {
                context.Students.Add(entity: item);
                await context.SaveChangesAsync();
            }
        }
    }
}