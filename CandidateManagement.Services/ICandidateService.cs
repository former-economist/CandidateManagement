namespace CandidateManagement.Services;

using CandidateManagement.Models;
public interface ICandidateService
{
    Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
    Task<Candidate?> GetCandidateByIdAsync(Guid id);
    Task<Candidate> CreateCandidateAsync(Candidate candidate);
    Task<Candidate> UpdateCandidateAsync(Candidate candidate);
    Task<Candidate> RemoveCandidateAsync(Guid id);
}
