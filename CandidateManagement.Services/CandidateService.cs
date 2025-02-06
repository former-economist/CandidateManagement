namespace CandidateManagement.Services;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CandidateManagement.Exceptions;
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

    public async Task<Candidate?> GetCandidateByIdAsync(Guid id)
    {
        var candidate = await _repository.GetCandidateByIdAsync(id);

        if (candidate == null) 
        {
            throw new RecordNotFoundException("Candidate not found");

        }
        return candidate;
    

    }

    public async Task<Candidate> CreateCandidateAsync(Candidate candidate)
    {
        ValidateCandidate(candidate);
        var existingCandidate = await _repository.GetCandidateByEmailAsync(candidate.Email);
        if (existingCandidate != null)
        {
            throw new ExistingRecordException("A candidate account already exist using given email");
        }
        
        return await _repository.AddCandidateAsync(candidate);
    }

    public async Task<Candidate> UpdateCandidateAsync(Candidate candidate)
    {   
        var isCandidateExist = await CheckIfCandidateExistsById(candidate.Id);
        if (isCandidateExist == null)
        {
            throw new RecordNotFoundException("Candidate not found");

        }
        ValidateCandidate(candidate);
        return await _repository.UpdateCandidateAsync(candidate);
    }

    public async Task<Candidate> RemoveCandidateAsync(Guid id)
    {
        var deletedCandidate = await _repository.DeleteCandidateAsync(id);
        if(deletedCandidate == null)
        {
            throw new RecordNotFoundException("Record not found, cannot be deleted");
        }
        var isCandidateStillExist = await CheckIfCandidateExistsById(id);

        if (isCandidateStillExist != null)
        {
            throw new RecordNotDeletedException("Record not deleted");
        }

        return deletedCandidate;
    }

    private void ValidateCandidate(Candidate candidate)
    {
        if (candidate == null)
        {
            throw new BadRequestException("Candidate instance is null");
        }
        if (string.IsNullOrWhiteSpace(candidate.Forename))
        {
            throw new BadRequestException("Forename is required");
        }
        if (string.IsNullOrWhiteSpace(candidate.Surname))
        {
            throw new BadRequestException("Surname is required");
        }
        if (string.IsNullOrWhiteSpace(candidate.Email))
        {
            throw new BadRequestException("Email not provided, please provide valid email.");
        }
        if (string.IsNullOrWhiteSpace(candidate.DateOfBirth.ToString()))
        {
            throw new BadRequestException("Date of birth not entered, provided date of birth.");
        }
        if (!CandidateIs18OrOver(candidate))
        {
            throw new BadRequestException("You are not old enough to use the service, candidate's must be 18 or over.");
        }
        var emailValid = Regex.IsMatch(candidate.Email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        if (!emailValid)
        {
            throw new BadRequestException("Email not provided, please provide valid email.");
        }
    }

    private bool CandidateIs18OrOver(Candidate candidate)
    {
        var minBirthDate = DateTime.UtcNow.Date.AddYears(-18);
        return candidate.DateOfBirth.Date <= minBirthDate ? true : false;
    }

    private async Task<Candidate?> CheckIfCandidateExistsById(Guid id)
    {
        return await _repository.GetCandidateByIdAsync(id); 
    }
}

