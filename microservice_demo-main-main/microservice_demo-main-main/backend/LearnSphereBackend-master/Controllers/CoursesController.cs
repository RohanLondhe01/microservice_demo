using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Api.DTOs;
using MyProject.Api.Models;
using MyProject.Api.Repositories.Interfaces;
using System.Text.Json;

namespace MyProject.Api.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly ICourseRepository _courses;

    public CoursesController(ICourseRepository courses) => _courses = courses;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll(CancellationToken ct)
    {
        var list = await _courses.GetAllAsync(ct);
        return Ok(list.Select(ToDto));
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<CourseDto>> GetBySlug(string slug, CancellationToken ct)
    {
        var course = await _courses.GetBySlugAsync(slug, ct);
        if (course == null) return NotFound();
        return Ok(ToDto(course));
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<ActionResult<CourseDto>> Create(CourseCreateUpdateDto req, CancellationToken ct)
    {
        if (await _courses.SlugExistsAsync(req.Slug, null, ct))
            return BadRequest("Slug already exists");

        var course = new Course
        {
            Title = req.Title,
            Slug = req.Slug,
            Summary = req.Summary,
            Description = req.Description,
            Thumbnail = req.Thumbnail,
            Categories = JsonSerializer.Serialize(req.Categories),
            Duration = req.Duration,
            Level = req.Level,
            Price = req.Price,
            StudentsCount = req.Students,
            Status = req.Status,
            AssessmentJson = req.Assessment != null ? JsonSerializer.Serialize(req.Assessment) : null,
            QuizJson = req.Quiz != null ? JsonSerializer.Serialize(req.Quiz) : null
        };

        await _courses.AddAsync(course, ct);
        await _courses.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetBySlug), new { slug = course.Slug }, ToDto(course));
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<CourseDto>> Update(Guid id, CourseCreateUpdateDto req, CancellationToken ct)
    {
        var course = await _courses.GetByIdAsync(id, ct);
        if (course == null) return NotFound();

        if (await _courses.SlugExistsAsync(req.Slug, id, ct))
            return BadRequest("Slug already exists");

        course.Title = req.Title;
        course.Slug = req.Slug;
        course.Summary = req.Summary;
        course.Description = req.Description;
        course.Thumbnail = req.Thumbnail;
        course.Categories = JsonSerializer.Serialize(req.Categories);
        course.Duration = req.Duration;
        course.Level = req.Level;
        course.Price = req.Price;
        course.StudentsCount = req.Students;
        course.Status = req.Status;
        course.AssessmentJson = req.Assessment != null ? JsonSerializer.Serialize(req.Assessment) : null;
        course.QuizJson = req.Quiz != null ? JsonSerializer.Serialize(req.Quiz) : null;

        _courses.Update(course);
        await _courses.SaveChangesAsync(ct);

        return Ok(ToDto(course));
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var course = await _courses.GetByIdAsync(id, ct);
        if (course == null) return NotFound();

        _courses.Delete(course);
        await _courses.SaveChangesAsync(ct);

        return NoContent();
    }

    private static CourseDto ToDto(Course c) => new()
    {
        Id = c.Id,
        Title = c.Title,
        Slug = c.Slug,
        Summary = c.Summary,
        Description = c.Description,
        Thumbnail = c.Thumbnail,
        Categories = JsonSerializer.Deserialize<List<string>>(c.Categories) ?? new(),
        Duration = c.Duration,
        Level = c.Level,
        Price = c.Price,
        Students = c.StudentsCount,
        Status = c.Status,
        Assessment = c.AssessmentJson != null ? JsonSerializer.Deserialize<object>(c.AssessmentJson) : null,
        Quiz = c.QuizJson != null ? JsonSerializer.Deserialize<object>(c.QuizJson) : null,
        CreatedAt = c.CreatedAt
    };
}
