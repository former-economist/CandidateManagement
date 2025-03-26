using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CandidateManagement.Infrastructure.Entity;
using CandidateManagement.Models;
using CandidateManagement.Repositories.Interfaces;
using CandidateManagement.Repositories.Repositories;
using CandidateManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CandidateManagement.Services.Services
{
    public class CentreService : ICentreService
    {
        private readonly ILogger _logger;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICentreRepository _centreRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseService _courseService;

        public CentreService(ICentreRepository repository, ICandidateRepository candidateRepository, ICourseRepository courseRepository, ILogger<CentreService> logger)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
            _courseRepository = courseRepository;
            _centreRepository = repository;
        }

        public async Task<Result<Centre>> CreateAsync(Centre centre)
        {
            var isValidCentre = ValidateCentre(centre);
            if (!isValidCentre.IsSuccess)
            {
                _logger.LogError($"{isValidCentre.ProblemDetails.Detail}");
                return isValidCentre;
            }
            var existingCentre = await _centreRepository.GetByEmailAsync(centre.Email);
            if (existingCentre != null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Duplicate Candidate",
                    Detail = $"Centre already exist with given email {centre.Email}",
                    Status = 400
                };

                _logger.LogError($"Canidate already exist with given email {centre.Email}");

                return Result<Centre>.Failure(problemDetails)!;
            }

            var addedCentre = await _centreRepository.AddCentreAsync(centre);

            _logger.LogInformation($"Added {addedCentre.Id}");

            return Result<Centre>.Success(centre);
        }

        public async Task<IEnumerable<Centre>> GetAllAsync()
        {
            _logger.LogInformation("Accessing all centres");

            return await _centreRepository.GetCentresWithCandidates();
            
        }

        public async Task<Result<Centre?>> GetByIdAsync(Guid id)
        {
            var centre = await _centreRepository.GetCentreByIdAsync(id);

            if (centre == null)
            {
                _logger.LogError($"Centre with ID {id} not found");
                var problemDetails = new ProblemDetails
                {
                    Title = "Centre not found",
                    Detail = $"Centre with ID {id} not found",
                    Status = 404
                };
                return Result<Centre>.Failure(problemDetails);
            }
            _logger.LogInformation("Centre Exists");
            return Result<Centre>.Success(centre)!;
        }

        public async Task<Result<Centre>> RemoveAsync(Centre centre)
        {
            var deletedCentreID = await _centreRepository.DeleteAsync(centre);
            if (deletedCentreID == Guid.Empty)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Centre not found",
                    Detail = $"Centre with ID {centre.Id} not found, cannot be deleted",
                    Status = 404
                };

                _logger.LogError($"Unable to remove centre: {centre.Id}");
                return Result<Centre>.Failure(problemDetails);

            }
            var isCentreStillExist = await CheckIfCentreExistsById(centre.Id);

            if (isCentreStillExist != null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Action not completed",
                    Detail = "Record not deleted",
                    Status = 500
                };

                _logger.LogError($"Record not deleted after attempt to delete");
                return Result<Centre>.Failure(problemDetails);

            }

            _logger.LogInformation($"Centre with ID {centre.Id} removed");
            return Result<Centre>.Success(centre);
        }

        public async Task<Result<Centre>> UpdateCentreCourses(Centre centre)
        {
            
            return Result<Centre>.Success(centre);
        }

        public async Task<Result<Centre>> UpdateAsync(Centre centre)
        {
            //var isCentreExist = await CheckIfCentreExistsById(centre.Id);
            //if (isCentreExist == null)
            //{
            //    var problemDetails = new ProblemDetails
            //    {
            //        Title = "Centre not found",
            //        Detail = $"Centre with ID {centre.Id} not found",
            //        Status = 404
            //    };

            //    _logger.LogError($"Centre with ID {centre.Id} not found");
            //    return Result<Centre>.Failure(problemDetails);

            //}
            ValidateCentre(centre);

            var updatedCentre = await _centreRepository.UpdateAsync(centre);

            _logger.LogInformation($"Centre with ID {centre.Id} update");
            return Result<Centre>.Success(updatedCentre);
        }

        public async Task<Result<Centre>> UpdateCentreCoures(Centre centre)
        {
            var courses = centre.Courses;

            foreach (var course in courses)
            {
                await _courseRepository.UpdateCourseCentresAsync(course, centre);
                
            }

            var updatedCentre = await _centreRepository.UpdateAsync(centre);

            return Result<Centre>.Success(updatedCentre);
        }

        private Result<Centre> ValidateCentre(Centre centre)
        {
            if (centre == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "No input data provided",
                    Detail = "Null Centre object provided",
                    Status = 400
                };

                _logger.LogError("Null Centre object");

                return Result<Centre>.Failure(problemDetails);
            }
            if (string.IsNullOrWhiteSpace(centre.Name))
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Insufficient Centre data provided",
                    Detail = "Centre Name not provided",
                    Status = 400
                };

                _logger.LogError("Centre Name not provided");

                return Result<Centre>.Failure(problemDetails);
            }
            if (string.IsNullOrWhiteSpace(centre.Email))
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Insufficient Centre data provided",
                    Detail = "Centre Email not provided",
                    Status = 400
                };

                _logger.LogError("Centre email not provided");

                return Result<Centre>.Failure(problemDetails);
            }
            if (string.IsNullOrWhiteSpace(centre.Address))
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Insufficient Centre data provided",
                    Detail = "Centre Address not provided",
                    Status = 400
                };

                _logger.LogError("Centre Address not provided");

                return Result<Centre>.Failure(problemDetails);
            }
            if (string.IsNullOrWhiteSpace(centre.TelephoneNumber))
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Insufficient Centre data provided",
                    Detail = "Centre Telephone Number not provided",
                    Status = 400
                };

                _logger.LogError("Centre Telephone Number not provided");

                return Result<Centre>.Failure(problemDetails);
            }


            return Result<Centre>.Success(centre);


        }

        private async Task<Centre?> CheckIfCentreExistsById(Guid id)
        {
            return await _centreRepository.GetByIdAsync(id);
        }
    }
}
