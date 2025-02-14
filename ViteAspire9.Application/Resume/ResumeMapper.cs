using ViteAspire9.Api.Database;

namespace ViteAspire9.Api.Features.Resume;

public static class ResumeMapper
{
	public static Education ToEducation(this EducationEntity educationEntity) =>
		new()
		{
			Degree = educationEntity.Degree,
			Description = educationEntity.Description,
			EndDate = educationEntity.EndDate,
			School = educationEntity.School,
			StartDate = educationEntity.StartDate
		};
	
	public static Experience ToExperience(this ExperienceEntity entity) =>
		new()
		{
			Title = entity.Title,
			Company = entity.Company,
			StartDate = entity.StartDate,
			EndDate = entity.EndDate,
			Description = entity.Description
		};
	
	public static ResumeData ToResumeData(this ResumeDataEntity entity) =>
		new()
		{
			Summary = entity.Summary,
			Experience = entity.Experience.Select(e => e.ToExperience()).ToList(),
			Education = entity.Education.Select(e => e.ToEducation()).ToList()
		};
	
	public static Resume ToResume(this ResumeEntity entity) =>
		new()
		{
			Id = entity.Id,
			Slug = entity.Slug,
			Name = entity.Name,
			Email = entity.Email,
			Phone = entity.Phone,
			Data = entity.DataEntity.ToResumeData()
		};
}