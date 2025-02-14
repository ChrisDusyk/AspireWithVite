using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViteAspire9.Api.Database;

public class ResumeEntity
{
	public Guid Id { get; set; }
	[MaxLength(50)]
	public string Slug { get; set; } = string.Empty;
	[MaxLength(100)]
	public string Name { get; set; } = string.Empty;
	[MaxLength(100)]
	public string Email { get; set; } = string.Empty;
	[MaxLength(15)]
	public string Phone { get; set; } = string.Empty;
	
	[Column(TypeName = "jsonb")]
	public ResumeDataEntity DataEntity { get; set; } = new();
}

public class ResumeDataEntity
{
	[MaxLength(200)]
	public string Summary { get; set; } = string.Empty;
	public List<ExperienceEntity> Experience { get; set; } = [];
	public List<EducationEntity> Education { get; set; } = [];
}

public class EducationEntity
{
	[MaxLength(100)]
	public string School { get; set; } = string.Empty;
	[MaxLength(100)]
	public string Degree { get; set; } = string.Empty;
	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	[MaxLength(200)]
	public string Description { get; set; } = string.Empty;
}

public class ExperienceEntity
{
	[MaxLength(100)]
	public string Title { get; set; } = string.Empty;
	[MaxLength(100)]
	public string Company { get; set; } = string.Empty;
	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	[MaxLength(500)]
	public string Description { get; set; } = string.Empty;
}