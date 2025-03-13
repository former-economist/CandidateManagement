namespace CandidateManagement.Repositories.Interfaces;

using CandidateManagement.Infrastructure.Entity;

public interface ICandidateRepository : IBaseRepository<Candidate>
{

    Task<Candidate> GetByEmailAsync(string email);
}
