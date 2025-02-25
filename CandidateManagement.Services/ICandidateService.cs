namespace CandidateManagement.Services;

using CandidateManagement.Models;
public interface ICandidateService
{
    Task<IEnumerable<Object>> GetAllCandidatesAsync();
    Task<Result<Object?>> GetCandidateByIdAsync(Guid id);
    Task<Result<Object>> CreateCandidateAsync(Object candidate);
    Task<Result<Object>> UpdateCandidateAsync(Object candidate);
    Task<Result<Object>> RemoveCandidateAsync(Guid id);
}
