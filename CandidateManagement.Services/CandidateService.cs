namespace CandidateManagement.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateManagement.Models;
using CandidateManagement.Repositories;

public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _repository;

    public CandidateService(ICandidateRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
    {
        return await _repository.GetCandidatesAsync();
    }

    public async Task<Candidate?> GetCandidateByIdAsync(int id)
    {
        return await _repository.GetCandidateByIdAsync(id);
    }

    public async Task<Candidate> CreateCandidateAsync(Candidate candidate)
    {
        return await _repository.AddCandidateAsync(candidate);
    }

    public async Task<Candidate> UpdateCandidateAsync(Candidate candidate)
    {
        return await _repository.UpdateCandidateAsync(candidate);
    }

    public async Task<Candidate> RemoveCandidateAsync(int id)
    {
        return await _repository.DeleteCandidateAsync(id);
    }
}

