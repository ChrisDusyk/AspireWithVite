using ViteAspire9.Api.Database;
using DbInitializer = ViteAspire9.Database.Migrator.DbInitializer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<DbInitializer>();

builder.AddServiceDefaults();

builder.AddResumeDatabase("resume");

var host = builder.Build();
host.Run();
