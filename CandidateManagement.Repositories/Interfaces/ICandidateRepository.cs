namespace CandidateManagement.Repositories.Interfaces;

using CandidateManagement.Infrastructure.Entity;

public interface ICandidateRepository : IBaseRepository<Registration>
{

    Task<Registration> GetByEmailAsync(string email);
}
