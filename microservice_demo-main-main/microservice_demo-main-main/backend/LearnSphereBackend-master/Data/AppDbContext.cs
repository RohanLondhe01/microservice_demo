using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyProject.Api.Models;

namespace MyProject.Api.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<User> Users => Set<User>();
	public DbSet<Student> Students => Set<Student>();
	public DbSet<Course> Courses => Set<Course>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Unique Email
		modelBuilder.Entity<User>()
			.HasIndex(u => u.Email)
			.IsUnique();

		// Unique Slug
		modelBuilder.Entity<Course>()
			.HasIndex(c => c.Slug)
			.IsUnique();

		// 1-1 relation: User <-> Student
		modelBuilder.Entity<User>()
			.HasOne(u => u.StudentProfile)
			.WithOne(s => s.User!)
			.HasForeignKey<Student>(s => s.UserId);

		// DateOnly conversion for SQL Server
		var converter = new ValueConverter<DateOnly?, DateTime?>(
			d => d.HasValue ? d.Value.ToDateTime(TimeOnly.MinValue) : null,
			d => d.HasValue ? DateOnly.FromDateTime(d.Value) : null
		);

		modelBuilder.Entity<Student>()
			.Property(s => s.DateOfBirth)
			.HasConversion(converter);

		base.OnModelCreating(modelBuilder);
	}
}