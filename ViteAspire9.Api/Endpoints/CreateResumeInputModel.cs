namespace ViteAspire9.Api.Features.Resume.Endpoints;

public class CreateResumeInputModel
{
	public required string Name { get; set; }
	public required string Email { get; set; }
	public required string Phone { get; set; }
	public required string Summary { get; set; }
	public required List<ExperienceInputModel> WorkExperiences { get; set; }
	public required List<EducationInputModel> Educations { get; set; }
}

public class ExperienceInputModel
{
	public required string Title { get; set; }
	public required string Company { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public required string Description { get; set; }
}

public class EducationInputModel
{
	public required string Degree { get; set; }
	public required string Description { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public required string School { get; set; }
}