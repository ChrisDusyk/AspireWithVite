using Scalar.AspNetCore;
using ViteAspire9.Api.Database;
using ViteAspire9.Api.Features.Resume.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddResumeDatabase("resume");
builder.Services.AddRepositories();

builder.Services.AddMediatR(config =>
	config.RegisterServicesFromAssembly(ViteAspire9.Application.AssemblyMarker.Assembly));
builder.Services.AddCors(opts => opts.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.AddServiceDefaults();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();

	/*DbInitializer.Initialize(app.Services);*/
}

app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.ConfigureResumeEndpoints();

app.UseCors();

app.Run();

public static partial class Program
{
}
