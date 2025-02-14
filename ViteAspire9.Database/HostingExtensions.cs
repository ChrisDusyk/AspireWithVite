using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ViteAspire9.Api.Database;

public static class HostingExtensions
{
	public static IServiceCollection AddRepositories(this IServiceCollection services) =>
		services.AddScoped<IResumeRepository, ResumeRepository>();

	public static void AddResumeDatabase(this IHostApplicationBuilder hostBuilder,
		string connectionName) =>
		hostBuilder.AddNpgsqlDbContext<ResumeDbContext>(connectionName);
}
