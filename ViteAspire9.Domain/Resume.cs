namespace ViteAspire9.Api.Features.Resume;

public record Resume
{
	public Guid Id { get; init; }
	public string Slug { get; init; } = string.Empty;
	public string Name { get; init; } = string.Empty;
	public string Email { get; init; } = string.Empty;
	public string Phone { get; init; } = string.Empty;
	public ResumeData Data { get; init; } = new();
}

public record ResumeData
{
	public string Summary { get; init; } = string.Empty;
	public List<Experience> Experience { get; init; } = [];
	public List<Education> Education { get; init; } = [];
}

public record Education
{
	public string School { get; init; } = string.Empty;
	public string Degree { get; init; } = string.Empty;
	public DateTime StartDate { get; init; }
	public DateTime? EndDate { get; init; }
	public string Description { get; init; } = string.Empty;
}

public record Experience
{
	public string Title { get; init; } = string.Empty;
	public string Company { get; init; } = string.Empty;
	public DateTime StartDate { get; init; }
	public DateTime? EndDate { get; init; }
	public string Description { get; init; } = string.Empty;
}