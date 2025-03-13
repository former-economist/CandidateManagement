namespace CandidateManagement.Services.Interfaces;

using CandidateManagement.Models;
using CandidateManagement.Infrastructure.Entity;
public interface ICandidateService
{
    Task<IEnumerable<Registration>> GetAllCandidatesAsync();
    Task<Result<Registration?>> GetCandidateByIdAsync(Guid id);
    Task<Result<Registration>> CreateCandidateAsync(Registration candidate);
    Task<Result<Registration>> UpdateCandidateAsync(Registration candidate);
    Task<Result<Registration>> RemoveCandidateAsync(Registration candidate);
}
