using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagement.Infrastructure;
using CandidateManagement.Infrastructure.Entity;
using CandidateManagement.Repositories.Interfaces;

namespace CandidateManagement.Repositories.Repositories
{
    public class CourseRepository : BaseRepository<Registration>, ICourseRepository
    {
        private readonly Context _context;
        public CourseRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
