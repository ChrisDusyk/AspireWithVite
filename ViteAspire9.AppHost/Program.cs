using ViteAspire9.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
	.AddPostgres("postgres")
	.WithDataVolume()
	.WithPgAdmin();

var db = postgres.AddDatabase("resume");

var dbMigrator = builder
	.AddProject<Projects.ViteAspire9_Database_Migrator>("dbMigrator")
	.WithReference(db)
	.WaitFor(db);

var api = builder
	.AddProject<Projects.ViteAspire9_Api>("api")
	.WithScalar()
	.WithReference(db)
	.WaitFor(db)
	.WaitFor(dbMigrator);

var ui = builder
	.AddPnpmApp("ui", "../ViteAspire9.UI", scriptName: "dev")
	.WithPnpmPackageInstallation()
	.WithHttpEndpoint(port: 3000, isProxied: false)
	.WithExternalHttpEndpoints()
	.WithReference(api)
	.WaitFor(api)
	.WithEnvironment("VITE_API_URL", api.GetEndpoint("https"))
	.WithEnvironment("PORT", "3000")
	.PublishAsDockerFile();

builder.Build().Run();
