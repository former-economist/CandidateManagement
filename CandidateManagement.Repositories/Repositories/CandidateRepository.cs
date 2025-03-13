namespace CandidateManagement.Repositories.Repositories;

using System.Threading.Tasks;
using CandidateManagement.Infrastructure;
using CandidateManagement.Infrastructure.Entity;
using CandidateManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CandidateRepository : BaseRepository<Registration>, ICandidateRepository
{
    private readonly Context _context;

    public CandidateRepository(Context context) : base(context)
    {
        _context = context;
    }

    public async Task<Registration?> GetByEmailAsync(string email)
    {
        return await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);
    }
}

