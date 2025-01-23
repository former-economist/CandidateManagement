namespace CandidateManagement.Repositories;

using CandidateManagement.Models;

public interface ICandidateRepository
{
    Task<IEnumerable<Candidate>> GetCandidatesAsync();
    Task<Candidate> GetCandidateByIdAsync(Guid id);
    Task<Candidate> AddCandidateAsync(Candidate candidate);
    Task<Candidate> UpdateCandidateAsync(Candidate candidate);
    Task<Candidate> DeleteCandidateAsync(Guid id);
}
