using CandidateManagement.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagement.Infrastructure
{
    public class Context : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Centre> Centres { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        private readonly DbContextOptions<Context> _dbContextOptions;

        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            

            //modelBuilder.Entity<Candidate>().HasData(candidate1, candidate2, candidate3, candidate4, candidate5, candidate6, candidate7, candidate8, candidate9, candidate10);
        }
    }
}
