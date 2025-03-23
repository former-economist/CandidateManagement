using CandidateManagement.Infrastructure;
using CandidateManagement.Infrastructure.Entity;

namespace CandidateManagement.Api
{
    public class SeedData
    {
        public static void Seed(Context context)
        {
            if (!context.Candidates.Any())

            {
                new Candidate
                {
                    Forename = "John",
                    Surname = "Smith",
                    Email = "john.smith@example.com",
                    DateOfBirth = new DateTime(1990, 3, 15),
                    SwqrNumber = "10012345",
                    TelephoneNumber = "0987654321",
                    Id = Guid.NewGuid()
                };

            
                 
                var centre = new Centre { Name = "Edinburgh Centre", Address = "123 Glasgow road", Email = "edinburgh@example.com", Certified = true, TelephoneNumber = "1234431212", };

                context.Candidates.AddRange(

                    

                );

                context.SaveChanges();

            }


        }
    }
}
