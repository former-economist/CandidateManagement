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
            await Assert.ThrowsAsync<RecordNotFoundException> (() => service.GetCandidateByIdAsync(Guid.NewGuid()));
            
        }

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
            return new Candidate
            {
                Id = Guid.NewGuid(),
                Forename = "Adam",
                Surname = "Smith",
                Email = "adam.smith@example.com",
                DateOfBirth = DateTime.Now,
                SwqrNumber = "10012345"
            };
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


