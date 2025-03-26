using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagement.Infrastructure.Entity;
using CandidateManagement.Models;
using CandidateManagement.Repositories.Interfaces;
using CandidateManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CandidateManagement.Services.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly ICentreRepository _courseRepository;
        private readonly ILogger _logger;

        public CourseService(ICourseRepository repository, ICentreRepository centreRepository, ILogger<CourseService> logger)
        {
            _repository = repository;
            _courseRepository = centreRepository;
            _logger = logger;
        }

        public async Task<Result<Course>> CreateCourseAsync(Course course)
        {
            var isValidCourse = ValidateCourse(course);

            if (!isValidCourse.IsSuccess) 
            {
                _logger.LogError($"{isValidCourse.ProblemDetails.Detail}");
                return isValidCourse;
            }

            _repository.AddAsync(course);

            return Result<Course>.Success(course);
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<Result<Course?>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Course>> RemoveAsync(Course course)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Course>> UpdateAsync(Course course)
        {
            throw new NotImplementedException();
        }

        private Result<Course> ValidateCourse(Course course)
        {
            if (course == null) 
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "No input data provided",
                    Detail = "Null Course object provided",
                    Status = 400
                };

                _logger.LogError("Null Course object");

                return Result<Course>.Failure(problemDetails);
            }
            if (string.IsNullOrWhiteSpace(course.Name))
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Insufficient course data provided",
                    Detail = "Course name not provided",
                    Status = 400
                };

                _logger.LogError("Course name not provided");

                return Result<Course>.Failure(problemDetails);
            }

            if (course.Centres.Count <= 0) 
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Insufficient course data provided",
                    Detail = "Centre not provided for course",
                    Status = 400
                };

                _logger.LogError("Centre not provided for course");
            }


            return Result<Course>.Success(course);
        }

        private async Task<Centre> CheckCentreExists(Centre centre) 
        {
            return await _courseRepository.GetByIdAsync(centre.Id);
        }
    }
}
