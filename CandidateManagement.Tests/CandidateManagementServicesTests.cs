using System.Net.WebSockets;
using CandidateManagement.Models;
using CandidateManagement.Repositories;
using CandidateManagement.Services;
using CandidateManagement.Exceptions;

using Moq;
using Microsoft.Extensions.Logging;
using System;

namespace CandidateManagement.Tests
{
    public class CandidateManagementServicesTests
    {
        [Fact]
        public async void GetAllCandidate_Gets_All_Candidates()
        {   
            var candidates = SampleCandidates();
            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.GetCandidatesAsync().Result)
                .Returns(candidates);

            var service = new CandidateService(mockRepository.Object, mockLogger.Object);

            var results = await service.GetAllCandidatesAsync();
            var resultList = results.ToList();

            Assert.NotNull(results);
            Assert.Equal(candidates.Count(), resultList.Count());
            for (int i = 0; i < candidates.Count; i++) {
                var expected = candidates[i];
                var actual = resultList[i];
                
                Assert.Equal(expected.Forename, actual.Forename);
            }

        }

        [Fact]
        public async void GetCandidateByID_Gets_Candidate_By_ID()
        {   
            //Arrange
            var candidate = SampleCandidate();
            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.GetCandidateByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(candidate);

            //Action
            var service = new CandidateService(mockRepository.Object, mockLogger.Object);
            var result = await service.GetCandidateByIdAsync(candidate.Id);


            Assert.NotNull(result);
            Assert.Equal(result.Value.Id, candidate.Id);
        }

        [Fact]
        public async void GetCandidateByID_Provides_A_Null()
        {
            //Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.GetCandidateByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as Candidate);


            //Action
            var service = new CandidateService(mockRepository.Object, mockLogger.Object);
            var guid = Guid.NewGuid();
            var outcome = service.GetCandidateByIdAsync(guid);
            //Assert
            Assert.NotEqual(outcome.Result, null);
            Assert.NotEqual(outcome.Result.ProblemDetails, null);
            Assert.Equal(outcome.Result.ProblemDetails.Detail, $"Candidate with ID {guid} not found");
            Assert.Equal(outcome.Result.ProblemDetails.Status, 404);
            Assert.Null(outcome.Result.Value);

        }



        [Fact]
        public async void CreateCandidateAsync_Creates_A_Candidate()
        {   
            //Arrange
            var candidate = SampleCandidate();
            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(candidate);

            //Action
            var service = new CandidateService(mockRepository.Object, mockLogger.Object);
            var outcome = await service.CreateCandidateAsync(candidate);

            Assert.NotNull(outcome);
            Assert.Equal(outcome.IsSuccess, true);
            Assert.Equal(outcome.Value.Id, candidate.Id);
            Assert.Equal(outcome.Value.SwqrNumber, candidate.SwqrNumber);
        }

        [Fact]
        public async void CreateCandidateAsync_Returns_400_For_Null_FOr_Null_Candidate()
        {
            //Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(null as Candidate);

            //Action
            var service = new CandidateService(mockRepository.Object, mockLogger.Object);
            var outcome = service.CreateCandidateAsync(null as Candidate);

            Assert.Equal(outcome.Result.IsSuccess, false);
            Assert.NotEqual(outcome.Result.ProblemDetails, null);
            Assert.Equal(outcome.Result.ProblemDetails.Detail, "Null Candidate object provided");
            Assert.Equal(outcome.Result.ProblemDetails.Status, 400);
            Assert.Null(outcome.Result.Value);
        }

        [Fact]
        public async void CreateCandidateAsync_Returns_400_For_No_Email()
        {
            var candidate = SampleCandidate();
            candidate.Email = string.Empty;
            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(null as Candidate);

            var service = new CandidateService(mockRepository.Object, mockLogger.Object);
            var outcome = service.CreateCandidateAsync(candidate);

            Assert.Equal(outcome.Result.IsSuccess, false);
            Assert.NotEqual(outcome.Result.ProblemDetails, null);
            Assert.Equal(outcome.Result.ProblemDetails.Detail, "Candidate Email not provided");
            Assert.Equal(outcome.Result.ProblemDetails.Status, 400);
            Assert.Null(outcome.Result.Value);
        }

        [Fact]
        public async void CreateCandidateAsync_Returns_400_For_Invalid_Email()
        {
            var candidate = SampleCandidate();
            candidate.Email = "exampletest";
            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(null as Candidate);

            var service = new CandidateService(mockRepository.Object, mockLogger.Object);

            var outcome = service.CreateCandidateAsync(candidate);

            Assert.Equal(outcome.Result.IsSuccess, false);
            Assert.NotEqual(outcome.Result.ProblemDetails, null);
            Assert.Equal(outcome.Result.ProblemDetails.Detail, "Invalid Candidate email provided");
            Assert.Equal(outcome.Result.ProblemDetails.Status, 400);
            Assert.Null(outcome.Result.Value);
        }

        [Fact]
        public async void CreateCandidateAsync_Returns_403_For_Underage_Candidate()
        {
            var candidate = SampleCandidate();
            candidate.DateOfBirth = DateTime.UtcNow;
            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(null as Candidate);

            var service = new CandidateService(mockRepository.Object, mockLogger.Object);
            var outcome = service.CreateCandidateAsync(candidate);

            Assert.Equal(outcome.Result.IsSuccess, false);
            Assert.NotEqual(outcome.Result.ProblemDetails, null);
            Assert.Equal(outcome.Result.ProblemDetails.Detail, "Users must be a minimum of 18 years old");
            Assert.Equal(outcome.Result.ProblemDetails.Status, 403);
            Assert.Null(outcome.Result.Value);
        }

        [Fact]
        public async void CreateCandidateAsync_Returns_403_For_Existing_Email()
        {
            var candidate = SampleCandidate();
            var existingCandidate = SampleCandidate();
            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.GetCandidateByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(existingCandidate);

            var service = new CandidateService(mockRepository.Object, mockLogger.Object);
            var outcome = service.CreateCandidateAsync(candidate);

            Assert.Null(outcome.Result.Value);
            Assert.Equal(false, outcome.Result.IsSuccess);
            Assert.NotEqual(null, outcome.Result.ProblemDetails);
            Assert.Equal("Canidate already exist with given email", outcome.Result.ProblemDetails.Detail);
            Assert.Equal(403, outcome.Result.ProblemDetails.Status);
        }

        [Fact]
        public async void UpdateCandidate_Updates_Candidate_Details()
        {

            var candidateToBeUpdated = SampleCandidate();
          
            Candidate updatedCandidate = new Candidate()
            {
                Id = candidateToBeUpdated.Id,
                Forename = "Troy",
                Surname = "Mclure",
                Email = "adam.smith@example.com",
                DateOfBirth = DateTime.UtcNow.Date.AddYears(-18),
                SwqrNumber = "10012345"
            };

            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.GetCandidateByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(candidateToBeUpdated);
            mockRepository.Setup(x => x.UpdateCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(updatedCandidate);

            var service = new CandidateService(mockRepository.Object, mockLogger.Object);
            var outcome = await service.UpdateCandidateAsync(candidateToBeUpdated);


            Assert.Equal(true, outcome.IsSuccess);
            Assert.NotNull(outcome);
            Assert.Equal(candidateToBeUpdated.Id, outcome.Value.Id);
            Assert.NotEqual("Adam", outcome.Value.Forename);
            Assert.NotEqual("Smith", outcome.Value.Surname);
            Assert.Equal("Troy", outcome.Value.Forename);
            Assert.Equal("Mclure", outcome.Value.Surname);
        }

        [Fact]
        public async void RemoveCandidate_Removes_Candidate()
        {
            var candidateToBeDeleted = SampleCandidate();

            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.DeleteCandidateAsync(It.IsAny<Guid>()))
                .ReturnsAsync(candidateToBeDeleted);

            var service = new CandidateService(mockRepository.Object, mockLogger.Object);
            
            var actualOutput = await service.RemoveCandidateAsync(candidateToBeDeleted.Id);

            Assert.NotNull(actualOutput);
            Assert.Equal(actualOutput.Value.Id, candidateToBeDeleted.Id);
        }

        [Fact]
        public async void RemoveCandidate_Raises_Exception_Non_Existent_Candidate()
        {
            var candidate = SampleCandidate();
            var mockRepository = new Mock<ICandidateRepository>();
            var mockLogger = new Mock<ILogger<CandidateService>>();
            mockRepository.Setup(x => x.DeleteCandidateAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as Candidate);

            var service = new CandidateService(mockRepository.Object, mockLogger.Object);
            var outcome = service.RemoveCandidateAsync(candidate.Id);

            Assert.Null(outcome.Result.Value);
            Assert.Equal(false, outcome.Result.IsSuccess);
            Assert.NotEqual(null, outcome.Result.ProblemDetails);
            Assert.Equal($"Candidate with ID {candidate.Id} not found, cannot be deleted", outcome.Result.ProblemDetails.Detail);
            Assert.Equal(404, outcome.Result.ProblemDetails.Status);
        }

        private Candidate SampleCandidate()
        {   
            Candidate candidate = new Candidate()
            {
                Id = Guid.NewGuid(),
                Forename = "Adam",
                Surname = "Smith",
                Email = "adam.smith@example.com",
                DateOfBirth = DateTime.UtcNow.Date.AddYears(-18),
                SwqrNumber = "10012345"
            };
            return candidate;
        }

        private List<Candidate> SampleCandidates()
        {
            List<Candidate> candidates = new List<Candidate> 
            {
                new Candidate
                {
                    Forename = "Adam",
                    Surname = "Smith",
                    Email = "adam.smith@example.com",
                    DateOfBirth = DateTime.Now,
                    SwqrNumber = "10012345"
                },
                new Candidate
                {
                    Forename = "Alec",
                    Surname = "Smith",
                    Email = "alec.smith@example.com",
                    DateOfBirth = DateTime.Now,
                    SwqrNumber = "10012344"
                },
                new Candidate
                {
                    Forename = "Barbara",
                    Surname = "Smith",
                    Email = "barbara.smith@example.com",
                    DateOfBirth = DateTime.Now,
                    SwqrNumber = "10012343"
                }
            };

            return candidates;
        }
    }
}


