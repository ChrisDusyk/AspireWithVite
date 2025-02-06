using Kontur.Results;
using MediatR;
using ViteAspire9.Api.Database;

namespace ViteAspire9.Api.Features.Resume.UseCases;

public class GetResumeBySlug
{
	public class Query : IRequest<Result<Error, Resume>>
	{
		public string Slug { get; set; }
	}
	
	public class Handler(IResumeRepository resumeRepository) : IRequestHandler<Query, Result<Error, Resume>>
	{
		public async Task<Result<Error, Resume>> Handle(Query request, CancellationToken cancellationToken)
		{
			ResumeEntity? resume;
			try
			{
				resume = await resumeRepository.GetResumeBySlugAsync(request.Slug, cancellationToken);
			}
			catch (Exception ex)
			{
				return Result.Fail(new Error("GetResumeBySlug.Error", ex.Message));
			}
			
			if (resume == null)
			{
				return Result.Fail(new Error("GetResumeBySlug.NotFound", "Resume not found"));
			}

			return Result.Succeed(resume.ToResume());
		}
	}
}