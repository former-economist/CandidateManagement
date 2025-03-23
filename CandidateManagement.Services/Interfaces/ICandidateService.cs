namespace CandidateManagement.Services.Interfaces;

using CandidateManagement.Models;
using CandidateManagement.Infrastructure.Entity;
public interface ICandidateService
{
    Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
    Task<Result<Candidate?>> GetCandidateByIdAsync(Guid id);
    Task<Result<Candidate>> CreateCandidateAsync(Candidate candidate);
    Task<Result<Candidate>> UpdateCandidateAsync(Candidate candidate);
    Task<Result<Candidate>> RemoveCandidateAsync(Candidate candidate);
}
