namespace CandidateManagement.Repositories;

using Microsoft.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using CandidateManagement.Infrastructure;
using CandidateManagement.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
{
    private readonly Context _context;

    public CandidateRepository(Context context) : base(context)
    {
        _context = context;
    }

    public async Task<Candidate?> GetByEmailAsync(string email)
    {
        return await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);
    }
}

