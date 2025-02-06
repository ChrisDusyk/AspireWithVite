using Kontur.Results;
using MediatR;
using ViteAspire9.Api.Database;

namespace ViteAspire9.Api.Features.Resume.UseCases;

public class GetResumeById
{
	public class Query : IRequest<Result<Error, Resume>>
	{
		public Guid Id { get; set; }
	}

	public class Handler(IResumeRepository resumeRepository) : IRequestHandler<Query, Result<Error, Resume>>
	{
		public async Task<Result<Error, Resume>> Handle(Query request, CancellationToken cancellationToken)
		{
			var resume = await resumeRepository.GetResumeAsync(request.Id, cancellationToken);
			if (resume == null)
			{
				return Result.Fail(new Error("Resume.NotFound", $"Resume not found for {request.Id}"));
			}

			return Result.Succeed(resume.ToResume());
		}
	}
}