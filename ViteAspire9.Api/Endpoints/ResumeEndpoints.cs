using Kontur.Results;
using MediatR;
using ViteAspire9.Api.Features.Resume.UseCases;

namespace ViteAspire9.Api.Features.Resume.Endpoints;

public static class ResumeEndpoints
{
	public static void ConfigureResumeEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("resume");
		
		group.MapGet("/get-resume/{id:Guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
		{
			var query = new GetResumeById.Query()
			{
				Id = id
			};
			var response = await sender.Send(query, cancellationToken: cancellationToken);
			
			return response.Match(
				error => Results.InternalServerError(error),
				Results.Ok
			);
		});
		
		group.MapGet("/get-resume-by-slug/{slug}", async (string slug, ISender sender, CancellationToken cancellationToken) =>
		{
			var query = new GetResumeBySlug.Query()
			{
				Slug = slug
			};
			var response = await sender.Send(query, cancellationToken: cancellationToken);
			
			return response.Match(
				error => Results.InternalServerError(error),
				Results.Ok
			);
		});
		
		group.MapPost("/create-resume", async (CreateResumeInputModel inputModel, ISender sender, CancellationToken cancellationToken) =>
		{
			var command = new CreateResume.Command()
			{
				Name = inputModel.Name,
				Email = inputModel.Email,
				Phone = inputModel.Phone,
				Summary = inputModel.Summary,
				WorkExperiences = inputModel.WorkExperiences.Select(e => new Experience
				{
					Title = e.Title,
					Company = e.Company,
					StartDate = e.StartDate,
					EndDate = e.EndDate,
					Description = e.Description
				}).ToList(),
				Educations = inputModel.Educations.Select(e => new Education
				{
					Degree = e.Degree,
					Description = e.Description,
					StartDate = e.StartDate,
					EndDate = e.EndDate,
					School = e.School
				}).ToList()
			};
			var response = await sender.Send(command, cancellationToken: cancellationToken);
			
			return response.Match(
				error => Results.InternalServerError(error),
				created => Results.Created($"/resume/get-resume/{created.Resume.Id}", created.Resume)
			);
		});
	}
}