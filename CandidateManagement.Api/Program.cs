using System.Text.Json.Serialization;
using CandidateManagement.Exceptions;
using CandidateManagement.Infrastructure;
using CandidateManagement.Infrastructure.Entity;
using CandidateManagement.Repositories.Interfaces;
using CandidateManagement.Repositories.Repositories;
using CandidateManagement.Services.Interfaces;
using CandidateManagement.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Logging.AddLog4Net();


// Register services 
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<ICentreService, CentreService>();
builder.Services.AddScoped<ICourseService, CourseService>();

// Register Db
builder.Services.AddDbContext<Context>(options =>
{
    options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = false;
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});

builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ICentreRepository, CentreRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages();
//app.UseExceptionHandler();

// Map endpoints 

app.MapGet("/course", async(ICourseService service) => await service.GetAllAsync());

app.MapGet("/centres", async(ICentreService service) => await service.GetAllAsync());

app.MapGet("/centres/{id}", async(Guid id,ICentreService service) => await service.GetByIdAsync(id));

app.MapPost("/centres", async (Centre centre, ICentreService service) => await service.CreateAsync(centre));

app.MapPut("/centre-courses", async (Centre centre, ICentreService service) => await service.UpdateCentreCoures(centre));

app.MapGet("/candidates", async (ICandidateService service) => await service.GetAllCandidatesAsync());

app.MapGet("/candidates/{id}", async (Guid id, ICandidateService service) =>
{
    await service.GetCandidateByIdAsync(id);

});

app.MapPost("/candidates", async ([FromBody] Candidate candidate, ICandidateService service) => await service.CreateCandidateAsync(candidate));

app.MapPut("/candidates", async (Candidate candidate, ICandidateService service) => await service.UpdateCandidateAsync(candidate));

app.MapDelete("/candidates1", async ([FromBody] Candidate candidate, ICandidateService service) => await service.RemoveCandidateAsync(candidate));



app.Run();



