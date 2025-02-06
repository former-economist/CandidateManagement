using CandidateManagement.Models;
using CandidateManagement.Repositories;
using CandidateManagement.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();


// Register services 

builder.Services.AddScoped<ICandidateRepository>(provider => new CandidateRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddExceptionHandler<ExceptionToProblemDetailsHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages();
app.UseExceptionHandler();

// Map endpoints 

app.MapGet("/candidates", async (ICandidateService service) => await service.GetAllCandidatesAsync());

app.MapGet("/candidates/{id}", async (Guid id, ICandidateService service) => await service.GetCandidateByIdAsync(id));

app.MapPost("/candidates", async (Candidate candidate, ICandidateService service) => await service.CreateCandidateAsync(candidate));

app.MapPut("/candidates", async (Candidate candidate, ICandidateService service) => await service.UpdateCandidateAsync(candidate));

app.MapDelete("/candidates/{id}", async (Guid id, ICandidateService service) => await service.RemoveCandidateAsync(id));



app.Run();