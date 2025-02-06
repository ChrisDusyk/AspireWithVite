using ViteAspire9.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
	.AddPostgres("postgres")
	.WithDataVolume()
	.WithPgAdmin();

var db = postgres.AddDatabase("resume");

var api = builder
	.AddProject<Projects.ViteAspire9_Api>("api")
	.WithScalar()
	.WithReference(db)
	.WaitFor(db);

var ui = builder
	.AddViteApp("ui", "../ViteAspire9.UI", packageManager: "pnpm")
	.WithPnpmPackageInstallation()
	.WithExternalHttpEndpoints()
	.WithReference(api)
	.WaitFor(api)
	.WithEnvironment("VITE_API_URL", api.GetEndpoint("https"));

builder.Build().Run();
