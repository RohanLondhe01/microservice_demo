namespace MyProject.Api.DTOs;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Slug { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Description { get; set; } = "";
    public string Thumbnail { get; set; } = "";
    public List<string> Categories { get; set; } = new();
    public string Duration { get; set; } = "";
    public string Level { get; set; } = "";
    public decimal Price { get; set; }
    public int Students { get; set; }
    public string Status { get; set; } = "";
    public object? Assessment { get; set; }
    public object? Quiz { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CourseCreateUpdateDto
{
    public string Title { get; set; } = "";
    public string Slug { get; set; } = "";
    public string Summary { get; set; } = "";
    public string Description { get; set; } = "";
    public string Thumbnail { get; set; } = "";
    public List<string> Categories { get; set; } = new();
    public string Duration { get; set; } = "";
    public string Level { get; set; } = "";
    public decimal Price { get; set; }
    public int Students { get; set; }
    public string Status { get; set; } = "";
    public object? Assessment { get; set; }
    public object? Quiz { get; set; }
}
