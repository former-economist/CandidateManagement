using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagement.Infrastructure;
using CandidateManagement.Infrastructure.Entity;
using CandidateManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagement.Repositories.Repositories
{
    public class CentreRepository : BaseRepository<Centre>, ICentreRepository
    {
        private readonly Context _context;
        public CentreRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Centre> GetCentreByIdAsync(Guid id)
        {
            return await _context.Centres.Where(centre => centre.Id == id).Include(centre => centre.Candidates).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Centre>> GetCentresWithCandidates()
        {
            return await _context.Centres.Include(centre => centre.Candidates).ToListAsync();
        }

        public async Task<Centre?> GetByEmailAsync(string email)
        {
            return await _context.Centres.SingleOrDefaultAsync(c => c.Email == email);

        }
    }
}
