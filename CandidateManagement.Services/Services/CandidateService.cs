namespace CandidateManagement.Services.Services;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CandidateManagement.Exceptions;
using CandidateManagement.Models;
using CandidateManagement.Infrastructure.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CandidateManagement.Repositories.Interfaces;
using CandidateManagement.Services.Interfaces;

public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _repository;
    private readonly ILogger _logger;

    public CandidateService(ICandidateRepository repository, ILogger<CandidateService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
    {
        _logger.LogInformation("Accessing all candidates");
        return await _repository.GetAllAsync();
    }

    public async Task<Result<Candidate?>> GetCandidateByIdAsync(Guid id)
    {
        var candidate = await _repository.GetByIdAsync(id);

        if (candidate == null)
        {
            _logger.LogError($"Candidate with ID {id} not found");
            var problemDetails = new ProblemDetails
            {
                Title = "Candidate not found",
                Detail = $"Candidate with ID {id} not found",
                Status = 404
            };
            return Result<Candidate>.Failure(problemDetails);
        }
        _logger.LogInformation("Candidate Exists");
        return Result<Candidate>.Success(candidate)!;


    }

    public async Task<Result<Candidate>> CreateCandidateAsync(Candidate candidate)
    {
        var isValidCandidate = ValidateCandidate(candidate);

        if (!isValidCandidate.IsSuccess)
        {
            _logger.LogError($"{isValidCandidate.ProblemDetails.Detail}");
            return isValidCandidate;
        }

        var existingCandidate = await _repository.GetByEmailAsync(candidate.Email);
        if (existingCandidate != null)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Duplicate Candidate",
                Detail = $"Canidate already exist with given email {candidate.Email}",
                Status = 400
            };

            _logger.LogError($"Canidate already exist with given email {candidate.Email}");

            return Result<Candidate>.Failure(problemDetails)!;
        }

        var addedCandidate = await _repository.AddAsync(candidate);

        _logger.LogInformation($"Added {addedCandidate.Id}");

        return Result<Candidate>.Success(candidate);
    }

    public async Task<Result<Candidate>> UpdateCandidateAsync(Candidate candidate)
    {
        var isCandidateExist = await CheckIfCandidateExistsById(candidate.Id);
        if (isCandidateExist == null)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Candidate not found",
                Detail = $"Candidate with ID {candidate.Id} not found",
                Status = 404
            };

            _logger.LogError($"Candidate with ID {candidate.Id} not found");
            return Result<Candidate>.Failure(problemDetails);

        }
        ValidateCandidate(candidate);

        var updatedCandidate = await _repository.UpdateAsync(candidate);

        _logger.LogInformation($"Candidate with ID {candidate.Id} update");
        return Result<Candidate>.Success(updatedCandidate);
    }

    public async Task<Result<Candidate>> RemoveCandidateAsync(Candidate candidate)
    {
        var deletedCandidateID = await _repository.DeleteAsync(candidate);
        if (deletedCandidateID == Guid.Empty)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Candidate not found",
                Detail = $"Candidate with ID {candidate.Id} not found, cannot be deleted",
                Status = 404
            };

            _logger.LogError($"Unable to remove candidate: {candidate.Id}");
            return Result<Candidate>.Failure(problemDetails);

        }
        var isCandidateStillExist = await CheckIfCandidateExistsById(candidate.Id);

        if (isCandidateStillExist != null)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Action not completed",
                Detail = "Record not deleted",
                Status = 500
            };

            _logger.LogError($"Record not deleted after attempt to delete");
            return Result<Candidate>.Failure(problemDetails);

        }

        _logger.LogInformation($"Candidate with ID {candidate.Id} removed");
        return Result<Candidate>.Success(candidate);
    }

    private Result<Candidate> ValidateCandidate(Candidate candidate)
    {
        if (candidate == null)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "No input data provided",
                Detail = "Null Candidate object provided",
                Status = 400
            };

            _logger.LogError("Null candidate object");

            return Result<Candidate>.Failure(problemDetails);
        }
        if (string.IsNullOrWhiteSpace(candidate.Forename))
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Insufficient candidate data provided",
                Detail = "Candidate Forename not provided",
                Status = 400
            };

            _logger.LogError("Candidate forename not provided");

            return Result<Candidate>.Failure(problemDetails);
        }
        if (string.IsNullOrWhiteSpace(candidate.Surname))
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Insufficient candidate data provided",
                Detail = "Candidate Surname not provided",
                Status = 400
            };

            _logger.LogError("Candidate surname not provided");

            return Result<Candidate>.Failure(problemDetails);
        }
        if (string.IsNullOrWhiteSpace(candidate.Email))
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Insufficient candidate data provided",
                Detail = "Candidate Email not provided",
                Status = 400
            };

            _logger.LogError("Candidate email not provided");

            return Result<Candidate>.Failure(problemDetails);
        }
        if (string.IsNullOrWhiteSpace(candidate.DateOfBirth.ToString()))
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Insufficient candidate data provided",
                Detail = "Candidate Date of Birth not provided",
                Status = 400
            };

            _logger.LogError("Candidate date of birth not provided");

            return Result<Candidate>.Failure(problemDetails);
        }
        if (!CandidateIs18OrOver(candidate))
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Forbidden: Age restriction",
                Detail = "Users must be a minimum of 18 years old",
                Status = 403
            };

            _logger.LogError("Candidate does not meet age restriction");

            return Result<Candidate>.Failure(problemDetails);
        }
        var emailValid = Regex.IsMatch(candidate.Email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        if (!emailValid)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Insufficient candidate data provided",
                Detail = "Invalid Candidate email provided",
                Status = 400
            };

            _logger.LogError("Candidate email is invalid format");

            return Result<Candidate>.Failure(problemDetails);
        }

        return Result<Candidate>.Success(candidate);
    }

    private bool CandidateIs18OrOver(Candidate candidate)
    {
        var minBirthDate = DateTime.UtcNow.Date.AddYears(-18);
        return candidate.DateOfBirth.Date <= minBirthDate ? true : false;
    }

    private async Task<Candidate?> CheckIfCandidateExistsById(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
}

