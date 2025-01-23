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
        var test = await _repository.GetCandidateByIdAsync(id);

        if (test == null) 
        {
            throw new RecordNotFoundException("Candidate not found");

        }
        return test;
    

    }

    public async Task<Candidate> CreateCandidateAsync(Candidate candidate)
    {   
        ValidateCandidate(candidate);
        return await _repository.AddCandidateAsync(candidate);
    }

    public async Task<Candidate> UpdateCandidateAsync(Candidate candidate)
    {
        ValidateCandidate(candidate);
        return await _repository.UpdateCandidateAsync(candidate);
    }

    public async Task<Candidate> RemoveCandidateAsync(Guid id)
    {   

        return await _repository.DeleteCandidateAsync(id);
    }

    protected bool ValidateCandidate(Candidate candidate)
    {
        if (candidate == null) {
            return false;
        }
        if (string.IsNullOrWhiteSpace(candidate.Forename)) {
            return false;
        }
        if (string.IsNullOrWhiteSpace(candidate.Surname)) { 
            return false;
        }
        if (string.IsNullOrWhiteSpace(candidate.Email)) {
            throw new BadRequestException("Email not provided, please provide valid email.");
        }
        if (string.IsNullOrWhiteSpace(candidate.DateOfBirth.ToString())) {
            return false;
        }

        try
        {
            var email = Regex.Replace(candidate.Email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            throw new Exception("Request timed out");
        }
        catch (ArgumentException e)
        {
            return false;
        }



        try
        {
            Regex.IsMatch(candidate.Email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            throw new Exception("Request timed out");
        }

        return true;

    }
}

