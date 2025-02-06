using Microsoft.EntityFrameworkCore;
using ViteAspire9.Api.Features.Resume;

namespace ViteAspire9.Api.Database;

public interface IResumeRepository
{
	ValueTask<ResumeEntity?> GetResumeAsync(Guid id, CancellationToken cancellationToken);
	Task<ResumeEntity?> GetResumeBySlugAsync(string slug, CancellationToken cancellationToken);
	ValueTask<ResumeEntity> CreateAsync(Resume resume, CancellationToken cancellationToken);
}

internal class ResumeRepository(ResumeDbContext context) : IResumeRepository
{
	public ValueTask<ResumeEntity?> GetResumeAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return context.Resumes.FindAsync([id], cancellationToken: cancellationToken);
	}
	
	public Task<ResumeEntity?> GetResumeBySlugAsync(string slug, CancellationToken cancellationToken = default)
	{
		return context.Resumes.SingleOrDefaultAsync(r => r.Slug == slug, cancellationToken);
	}

	public async ValueTask<ResumeEntity> CreateAsync(Resume resume, CancellationToken cancellationToken)
	{
		var entity = resume.MapToEntity();
		await context.Resumes.AddAsync(entity, cancellationToken);
		await context.SaveChangesAsync(cancellationToken);
		return entity;
	}
}

internal static class EntityMapper
{
	public static ResumeEntity MapToEntity(this Resume resume) =>
		new()
		{
			Id = resume.Id,
			Slug = resume.Slug,
			Name = resume.Name,
			Email = resume.Email,
			Phone = resume.Phone,
			DataEntity = new ResumeDataEntity
			{
				Summary = resume.Data.Summary,
				Experience = resume.Data.Experience.Select(e => e.MapToEntity()).ToList(),
				Education = resume.Data.Education.Select(e => e.MapToEntity()).ToList()
			}
		};

	public static ExperienceEntity MapToEntity(this Experience experience) =>
		new()
		{
			Title = experience.Title,
			Company = experience.Company,
			StartDate = experience.StartDate,
			EndDate = experience.EndDate,
			Description = experience.Description
		};

	public static EducationEntity MapToEntity(this Education education) =>
		new()
		{
			Degree = education.Degree,
			Description = education.Description,
			EndDate = education.EndDate,
			School = education.School,
			StartDate = education.StartDate
		};
}