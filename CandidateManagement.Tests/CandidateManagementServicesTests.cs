using System.Net.WebSockets;
using CandidateManagement.Models;
using CandidateManagement.Repositories;
using CandidateManagement.Services;
using CandidateManagement.Exceptions;

using Moq; 

namespace CandidateManagement.Tests
{
    public class CandidateManagementServicesTests
    {
        [Fact]
        public async void GetAllCandidate_Gets_All_Candidates()
        {   
            var candidates = SampleCandidates();
            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(x => x.GetCandidatesAsync().Result)
                .Returns(candidates);

            var service = new CandidateService(mockRepository.Object);

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
            mockRepository.Setup(x => x.GetCandidateByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(candidate);

            //Action
            var service = new CandidateService(mockRepository.Object);
            var result = await service.GetCandidateByIdAsync(candidate.Id);


            Assert.NotNull(result);
            Assert.Equal(result.Id, candidate.Id);
        }

        [Fact]
        public async void GetCandidateByID_Provides_A_Null()
        {
            //Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(x => x.GetCandidateByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as Candidate);


            //Action
            var service = new CandidateService(mockRepository.Object);

            //Assert
            await Assert.ThrowsAsync<RecordNotFoundException> (() => service.GetCandidateByIdAsync(Guid.NewGuid()));
            
        }



        [Fact]
        public async void CreateCandidateAsync_Creates_A_Candidate()
        {   
            //Arrange
            var candidate = SampleCandidate();
            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(candidate);

            //Action
            var service = new CandidateService(mockRepository.Object);
            var outcome = await service.CreateCandidateAsync(candidate);

            Assert.NotNull(outcome);
            Assert.Equal(outcome.Id, candidate.Id);
            Assert.Equal(outcome.SwqrNumber, candidate.SwqrNumber);
        }

        [Fact]
        public async void CreateCandidateAsync_Throws_Exception_For_Null_FOr_Null_Candidate()
        {
            //Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(null as Candidate);

            //Action
            var service = new CandidateService(mockRepository.Object);
            
            await Assert.ThrowsAsync<BadRequestException>(() => service.CreateCandidateAsync(null as Candidate));
        }

        [Fact]
        public async void CreateCandidateAsync_Throws_Exception_For_No_Email()
        {
            var candidate = SampleCandidate();
            candidate.Email = string.Empty;
            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(null as Candidate);

            var service = new CandidateService(mockRepository.Object);

            await Assert.ThrowsAsync<BadRequestException>(() => service.CreateCandidateAsync(candidate));
        }

        [Fact]
        public async void CreateCandidateAsync_Throws_Exception_For_Invalid_Email()
        {
            var candidate = SampleCandidate();
            candidate.Email = "exampletest";
            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(null as Candidate);

            var service = new CandidateService(mockRepository.Object);

            await Assert.ThrowsAsync<BadRequestException>(() => service.CreateCandidateAsync(candidate));
        }

        [Fact]
        public async void CreateCandidateAsync_Throws_Exception_For_Underage_Candidate()
        {
            var candidate = SampleCandidate();
            candidate.DateOfBirth = DateTime.UtcNow;
            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(null as Candidate);

            var service = new CandidateService(mockRepository.Object);

            await Assert.ThrowsAsync<BadRequestException>(() => service.CreateCandidateAsync(candidate));
        }

        public async void CreateCandidateAsync_Throws_Exception_For_Existing_Email()
        {
            var candidate = SampleCandidate();
            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(x => x.GetCandidateByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(null as Candidate);

            var service = new CandidateService(mockRepository.Object);

            await Assert.ThrowsAsync<ExistingRecordException>(() => service.CreateCandidateAsync(candidate));
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
            mockRepository.Setup(x => x.GetCandidateByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(candidateToBeUpdated);
            mockRepository.Setup(x => x.UpdateCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(updatedCandidate);

            var service = new CandidateService(mockRepository.Object);
            var actual = await service.UpdateCandidateAsync(candidateToBeUpdated);



            Assert.NotNull(actual);
            Assert.Equal(candidateToBeUpdated.Id, actual.Id);
            Assert.NotEqual("Adam", actual.Forename);
            Assert.NotEqual("Smith", actual.Surname);
            Assert.Equal("Troy", actual.Forename);
            Assert.Equal("Mclure", actual.Surname);
        }

        [Fact]
        public async void RemoveCandidate_Removes_Candidate()
        {
            var candidateToBeDeleted = SampleCandidate();

            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(x => x.DeleteCandidateAsync(It.IsAny<Guid>()))
                .ReturnsAsync(candidateToBeDeleted);

            var service = new CandidateService(mockRepository.Object);
        }
        //[Fact]
        //public async void DeleteCandidate_Deletes_Candidate()
        //{
        //    var candidate = SampleCandidate();
        //    var mockRepository = new Mock<ICandidateRepository>();
        //    mockRepository.Setup(x => x.DeleteCandidateAsync(It.IsAny<Guid>()))
        //        .ReturnsAsync();
        //}
        //// Example: Service Test in C#
        //[Fact]
        //public async Task GetProductById_ReturnsCorrectProduct()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IProductRepository>();
        //    mockRepo.Setup(repo => repo.GetByIdAsync(1))
        //            .ReturnsAsync(new Product { Id = 1, Name = "TestProduct" });

        //    var service = new ProductService(mockRepo.Object);

        //    // Act
        //    var product = await service.GetProductByIdAsync(1);

        //    // Assert
        //    Assert.NotNull(product);
        //    Assert.Equal("TestProduct", product.Name);
        //}

        //[Fact]
        //public async Task GetProductById_ReturnsProductFromDatabase()
        //{
        //    // Arrange
        //    using var context = new InMemoryDbContext();
        //    context.Products.Add(new Product { Id = 1, Name = "TestProduct" });
        //    await context.SaveChangesAsync();

        //    var repository = new ProductRepository(context);

        //    // Act
        //    var product = await repository.GetByIdAsync(1);

        //    // Assert
        //    Assert.NotNull(product);
        //    Assert.Equal("TestProduct", product.Name);
        //}

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


