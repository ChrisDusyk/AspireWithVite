using Kontur.Results;
using MediatR;
using ViteAspire9.Api.Database;

namespace ViteAspire9.Api.Features.Resume.UseCases;

public static class CreateResume
{
	public class Command : IRequest<Result<Error, Response>>
	{
		public required string Name { get; init; }
		public required string Email { get; init; }
		public required string Phone { get; init; }
		public required string Summary { get; init; }
		public required List<Experience> WorkExperiences { get; init; }
		public required List<Education> Educations { get; init; }
	}

	public class Response
	{
		public required Resume Resume { get; init; }
	}

	public class Handler(IResumeRepository resumeRepository) : IRequestHandler<Command, Result<Error, Response>>
	{
		public async Task<Result<Error, Response>> Handle(Command command, CancellationToken cancellationToken)
		{
			var resume = new Resume
			{
				Id = Guid.CreateVersion7(),
				Slug = GenerateSlug(command.Name),
				Name = command.Name,
				Email = command.Email,
				Phone = command.Phone,
				Data = new ResumeData()
				{
					Summary = command.Summary,
					Experience = command.WorkExperiences,
					Education = command.Educations,
				}
			};

			ResumeEntity newEntity;
			try
			{
				newEntity = await resumeRepository.CreateAsync(resume, cancellationToken);
			}
			catch (Exception e)
			{
				return Result.Fail(new Error("Persistence.Error", e.Message));
			}

			return Result.Succeed(new Response { Resume = newEntity.ToResume() });
		}

		private static string GenerateSlug(string name)
			=> name.ToLower().Replace(' ', '-') + "-" + Guid.NewGuid().ToString()[..5];
	}
}
