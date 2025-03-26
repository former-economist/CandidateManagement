using CandidateManagement.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagement.Infrastructure
{
    public class Context : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Centre> Centres { get; set; }
        public DbSet<Course> Courses { get; set; }
        //public DbSet<Registration> Registrations { get; set; }

        private readonly DbContextOptions<Context> _dbContextOptions;

        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            var centre = new Centre
            {
                Id = Guid.NewGuid(),
                Name = "Tech Skills Academy",
                Address = "45 Tech Avenue, Manchester, UK",
                Email = "contact@techskills.com",
                Certified = true,
                TelephoneNumber = "0161-9876-5432"
            };

            modelBuilder.Entity<Centre>().HasData(centre);

            var candidate = new Candidate { Forename = "John", Surname = "Smith", Email = "john.smith@example.com", DateOfBirth = new DateTime(1990, 3, 15), SwqrNumber = "10012345", TelephoneNumber = "0987654321", Id = Guid.NewGuid(), CentreID = centre.Id };

            modelBuilder.Entity<Candidate>().HasData(candidate);

            var course = new Course { Id = Guid.NewGuid(), Name = "Computer Science"};

            modelBuilder.Entity<Course>().HasData(course);

            
        }
    }
}
