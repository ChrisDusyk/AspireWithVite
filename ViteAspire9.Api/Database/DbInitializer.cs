using Microsoft.EntityFrameworkCore;

namespace ViteAspire9.Api.Database;

internal static class DbInitializer
{
	public static void Initialize(IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<ResumeDbContext>();
		context.Database.Migrate();
	}
}