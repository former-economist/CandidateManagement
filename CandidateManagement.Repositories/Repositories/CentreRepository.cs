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

        public async Task<Centre> AddCentreAsync(Centre centre)
        {
            await _context.AddAsync(centre);
            await _context.SaveChangesAsync();
            var returnedCentre = await _context.Centres.Where(c => c.Id == centre.Id).Include(c => c.Candidates).FirstOrDefaultAsync();

            return returnedCentre;

        }

        public async Task<Centre> GetCentreByIdAsync(Guid id)
        {
            return await _context.Centres.Where(centre => centre.Id == id).Include(centre => centre.Candidates).Include(centre => centre.Courses).FirstOrDefaultAsync();
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
