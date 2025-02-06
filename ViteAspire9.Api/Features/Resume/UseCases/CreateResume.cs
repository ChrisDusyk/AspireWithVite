using Kontur.Results;
using MediatR;
using ViteAspire9.Api.Database;

namespace ViteAspire9.Api.Features.Resume.UseCases;

public class CreateResume
{
	public class Command : IRequest<Result<Error, Response>>
	{
		public required string Name { get; set; }
		public required string Email { get; set; }
		public required string Phone { get; set; }
		public required string Summary { get; set; }
		public required List<Experience> WorkExperiences { get; set; }
		public required List<Education> Educations { get; set; }
	}

	public class Response
	{
		public required Resume Resume { get; set; }
	}

	public class Handler : IRequestHandler<Command, Result<Error, Response>>
	{
		private readonly IResumeRepository _resumeRepository;

		public Handler(IResumeRepository resumeRepository)
		{
			_resumeRepository = resumeRepository;
		}

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
				newEntity = await _resumeRepository.CreateAsync(resume, cancellationToken);
			}
			catch (Exception e)
			{
				return Result.Fail(new Error("Persistence.Error", e.Message));
			}

			return Result.Succeed(new Response { Resume = newEntity.ToResume() });
		}
		
		private static string GenerateSlug(string name)
		{
			return name.ToLower().Replace(' ', '-') + "-" + Guid.NewGuid().ToString()[..5];
		}
	}
}