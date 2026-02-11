using System.ComponentModel.DataAnnotations;

namespace MyProject.Api.Models;

public class Course
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Summary { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Thumbnail { get; set; } = string.Empty;

    public string Categories { get; set; } = "[]"; // Store as JSON string

    public string Duration { get; set; } = string.Empty;

    public string Level { get; set; } = "beginner";

    public decimal Price { get; set; } = 0;

    public int StudentsCount { get; set; } = 0;

    public string Status { get; set; } = "published"; // "published" or "draft"

    public string? AssessmentJson { get; set; }

    public string? QuizJson { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
