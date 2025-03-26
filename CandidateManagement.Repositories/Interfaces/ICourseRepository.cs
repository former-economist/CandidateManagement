using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagement.Infrastructure.Entity;

namespace CandidateManagement.Repositories.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllCoursesAndCentresAsync();

        Task UpdateCourseCentresAsync(Course course, Centre newCentre);
    }
}
