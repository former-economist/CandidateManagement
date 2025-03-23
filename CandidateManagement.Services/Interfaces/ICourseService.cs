using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagement.Infrastructure.Entity;
using CandidateManagement.Models;

namespace CandidateManagement.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Result<Course?>> GetByIdAsync(Guid id);
        Task<Result<Course>> CreateAsync(Course course);
        Task<Result<Course>> UpdateAsync(Course course);
        Task<Result<Course>> RemoveAsync(Course course);
    }
}
