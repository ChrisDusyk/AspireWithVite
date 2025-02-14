using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ViteAspire9.Api.Database;

namespace ViteAspire9.Database.Migrator;

public class DbInitializer(IServiceProvider serviceProvider, IHostEnvironment hostEnvironment, IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
	private readonly ActivitySource _activitySource = new(hostEnvironment.ApplicationName);

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		using var activity = _activitySource.StartActivity(hostEnvironment.ApplicationName, ActivityKind.Client);

		try
		{
			using var scope = serviceProvider.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<ResumeDbContext>();

			await EnsureDatabaseAsync(context, stoppingToken);
			await MigrateDatabaseAsync(context, stoppingToken);
		}
		catch (Exception ex)
		{
			activity?.AddException(ex);
			throw;
		}

		hostApplicationLifetime.StopApplication();
	}

	private static async Task EnsureDatabaseAsync(ResumeDbContext context, CancellationToken cancellationToken)
	{
		var dbCreator = context.Database.GetService<IRelationalDatabaseCreator>();

		var strategy = context.Database.CreateExecutionStrategy();
		await strategy.ExecuteAsync(async ct =>
		{
			if (!await dbCreator.ExistsAsync(ct))
			{
				await dbCreator.CreateAsync(ct);
			}
		}, cancellationToken);
	}

	private static async Task MigrateDatabaseAsync(ResumeDbContext context, CancellationToken cancellationToken)
	{
		var strategy = context.Database.CreateExecutionStrategy();
		await strategy.ExecuteAsync(async ct =>
		{
			/*await using var transaction = await context.Database.BeginTransactionAsync(ct);*/
			await context.Database.MigrateAsync(ct);
			/*await transaction.CommitAsync(ct);*/
		}, cancellationToken);
	}
}
