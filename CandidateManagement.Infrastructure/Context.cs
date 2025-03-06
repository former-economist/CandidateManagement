using CandidateManagement.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagement.Infrastructure
{
    public class Context : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }

        private readonly DbContextOptions<Context> _dbContextOptions;

        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var candidate1 = new Candidate { Forename = "John", Surname = "Smith", Email = "john.smith@example.com", DateOfBirth = new DateTime(1990, 3, 15), SwqrNumber = "10012345", TelephoneNumber = "0987654321", Id = Guid.NewGuid() };
            var candidate2 = new Candidate { Forename = "Jane", Surname = "Doe", Email = "jane.doe@example.com", DateOfBirth = new DateTime(1985, 07, 22), SwqrNumber = "10023456", TelephoneNumber = "1234567890", Id = Guid.NewGuid() };
            var candidate3 = new Candidate { Forename = "Michael", Surname = "Johnson", Email = "michael.johnson@example.com", DateOfBirth = new DateTime(1992, 11, 05), SwqrNumber = "10034567", TelephoneNumber = "9876543210", Id = Guid.NewGuid() };
            var candidate4 = new Candidate { Forename = "Emily", Surname = "Davis", Email = "emily.davis@example.com", DateOfBirth = new DateTime(1988, 06, 18), SwqrNumber = "10045678", TelephoneNumber = "5551234567", Id = Guid.NewGuid() };
            var candidate5 = new Candidate { Forename = "Robert", Surname = "Brown", Email = "robert.brown@example.com", DateOfBirth = new DateTime(1995, 04, 10), SwqrNumber = "10056789", TelephoneNumber = "5559876543", Id = Guid.NewGuid() };
            var candidate6 = new Candidate { Forename = "Sarah", Surname = "Wilson", Email = "sarah.wilson@example.com", DateOfBirth = new DateTime(1990, 09, 25), SwqrNumber = "10067890", TelephoneNumber = "4443217890", Id = Guid.NewGuid() };
            var candidate7 = new Candidate { Forename = "David", Surname = "Martinez", Email = "david.martinez@example.com", DateOfBirth = new DateTime(1987, 12, 30), SwqrNumber = "10078901", TelephoneNumber = "3335671234", Id = Guid.NewGuid() };
            var candidate8 = new Candidate { Forename = "Laura", Surname = "Anderson", Email = "laura.anderson@example.com", DateOfBirth = new DateTime(1993, 05, 14), SwqrNumber = "10089012", TelephoneNumber = "2226549876", Id = Guid.NewGuid() };
            var candidate9 = new Candidate { Forename = "James", Surname = "Taylor", Email = "james.taylor@example.com", DateOfBirth = new DateTime(1989, 08, 20), SwqrNumber = "10190123", TelephoneNumber = "1117896543", Id = Guid.NewGuid() };
            var candidate10 = new Candidate { Forename = "Olivia", Surname = "Harris", Email = "olivia.harris@example.com", DateOfBirth = new DateTime(1996, 02, 28), SwqrNumber = "10201234", TelephoneNumber = "9991239876", Id = Guid.NewGuid() };

            modelBuilder.Entity<Candidate>().HasData(candidate1, candidate2, candidate3, candidate4, candidate5, candidate6, candidate7, candidate8, candidate9, candidate10);
        }
    }
}
