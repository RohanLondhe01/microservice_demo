using Microsoft.EntityFrameworkCore;
using MyProject.Api.Data;
using MyProject.Api.Models;
using MyProject.Api.Repositories.Interfaces;

namespace MyProject.Api.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Course>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Courses
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task<Course?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Courses.FindAsync(new object[] { id }, ct);
    }

    public async Task<Course?> GetBySlugAsync(string slug, CancellationToken ct = default)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.Slug == slug, ct);
    }

    public async Task AddAsync(Course course, CancellationToken ct = default)
    {
        await _context.Courses.AddAsync(course, ct);
    }

    public void Update(Course course)
    {
        _context.Courses.Update(course);
    }

    public void Delete(Course course)
    {
        _context.Courses.Remove(course);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken ct = default)
    {
        return await _context.Courses.AnyAsync(c => c.Slug == slug && c.Id != excludeId, ct);
    }
}
