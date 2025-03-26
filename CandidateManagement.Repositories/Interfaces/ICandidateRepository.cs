namespace CandidateManagement.Repositories.Interfaces;

using CandidateManagement.Infrastructure.Entity;

public interface ICandidateRepository : IBaseRepository<Candidate>
{
    Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
    Task<Candidate> GetByEmailAsync(string email);
}
