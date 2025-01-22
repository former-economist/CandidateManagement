using CandidateManagement.Repositories;
using Moq;

namespace CandidateManagement.Tests
{
    public class CandidateManagementServices
    {
        [Fact]
        public void AddCandidate_adds_candidate_to_database()
        {
            var mockRepository = new Mock<ICandidateRepository>();
        }
    }
}