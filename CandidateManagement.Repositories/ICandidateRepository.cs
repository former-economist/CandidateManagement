namespace CandidateManagement.Repositories;

using CandidateManagement.Infrastructure.Entity;

public interface ICandidateRepository
{
    
    Task<Candidate> GetByEmailAsync(string email);
}
