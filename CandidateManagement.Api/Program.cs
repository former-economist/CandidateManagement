using CandidateManagement.Exceptions;
using CandidateManagement.Infrastructure;
using CandidateManagement.Infrastructure.Entity;
using CandidateManagement.Repositories;
using CandidateManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Logging.AddLog4Net();


// Register services 

builder.Services.AddScoped<ICandidateRepository>(provider => new CandidateRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICandidateService, CandidateService>();
//builder.Services.AddExceptionHandler<ExceptionToProblemDetailsHandler>();
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


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

app.MapGet("/candidates/{id}", async (Guid id, ICandidateService service) => {
    return Results.Ok(await service.GetCandidateByIdAsync(id));
    //try
    //{
    //    return Results.Ok(await service.GetCandidateByIdAsync(id));
    //}
    //catch (RecordNotFoundException ex) {
    //    return Results.NotFound("Candidate not found");
    //}
});

app.MapPost("/candidates", async (Candidate candidate, ICandidateService service) => await service.CreateCandidateAsync(candidate));

app.MapPut("/candidates", async (Candidate candidate, ICandidateService service) => await service.UpdateCandidateAsync(candidate));

app.MapDelete("/candidates/{id}", async (Guid id, ICandidateService service) => await service.RemoveCandidateAsync(id));



app.Run();

void SeedData(Context context)

{

    if (!context.Candidates.Any())

    {

        context.Candidates.AddRange(

            new Candidate { Forename = "John", Surname = "Smith", Email = "john.smith@example.com", DateOfBirth = new DateTime(1990, 3, 15), SwqrNumber = "10012345", TelephoneNumber = "0987654321", Id = Guid.NewGuid() },

            new Candidate { Forename = "Jane", Surname = "Doe", Email = "jane.doe@example.com", DateOfBirth = new DateTime(1985, 7, 22), SwqrNumber = "10023456", TelephoneNumber = "1234567890", Id = Guid.NewGuid() },

            new Candidate { Forename = "Michael", Surname = "Johnson", Email = "michael.johnson@example.com", DateOfBirth = new DateTime(1992, 11, 5), SwqrNumber = "10034567", TelephoneNumber = "9876543210", Id = Guid.NewGuid() },

            new Candidate { Forename = "Emily", Surname = "Davis", Email = "emily.davis@example.com", DateOfBirth = new DateTime(1988, 6, 18), SwqrNumber = "10045678", TelephoneNumber = "5551234567", Id = Guid.NewGuid() },

            new Candidate { Forename = "Robert", Surname = "Brown", Email = "robert.brown@example.com", DateOfBirth = new DateTime(1995, 4, 10), SwqrNumber = "10056789", TelephoneNumber = "5559876543", Id = Guid.NewGuid() },

            new Candidate { Forename = "Sarah", Surname = "Wilson", Email = "sarah.wilson@example.com", DateOfBirth = new DateTime(1990, 9, 25), SwqrNumber = "10067890", TelephoneNumber = "4443217890", Id = Guid.NewGuid() },

            new Candidate { Forename = "David", Surname = "Martinez", Email = "david.martinez@example.com", DateOfBirth = new DateTime(1987, 12, 30), SwqrNumber = "10078901", TelephoneNumber = "3335671234", Id = Guid.NewGuid() },

            new Candidate { Forename = "Laura", Surname = "Anderson", Email = "laura.anderson@example.com", DateOfBirth = new DateTime(1993, 5, 14), SwqrNumber = "10089012", TelephoneNumber = "2226549876", Id = Guid.NewGuid() },

            new Candidate { Forename = "James", Surname = "Taylor", Email = "james.taylor@example.com", DateOfBirth = new DateTime(1989, 8, 20), SwqrNumber = "10190123", TelephoneNumber = "1117896543", Id = Guid.NewGuid() },

            new Candidate { Forename = "Olivia", Surname = "Harris", Email = "olivia.harris@example.com", DateOfBirth = new DateTime(1996, 2, 28), SwqrNumber = "10201234", TelephoneNumber = "9991239876", Id = Guid.NewGuid() }

        );

        context.SaveChanges();

    }

}

