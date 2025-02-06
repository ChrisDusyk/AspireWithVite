using Microsoft.EntityFrameworkCore;

namespace ViteAspire9.Api.Database;

public class ResumeDbContext(DbContextOptions<ResumeDbContext> options) : DbContext (options)
{
	public DbSet<ResumeEntity> Resumes { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ResumeEntity>()
			.OwnsOne(r => r.DataEntity, d =>
			{
				d.ToJson();
				d.OwnsMany(r => r.Education);
				d.OwnsMany(r => r.Experience);
			});
	}
}