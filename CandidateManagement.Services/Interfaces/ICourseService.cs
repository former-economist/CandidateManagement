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
        Task<IEnumerable<Registration>> GetAllAsync();
        Task<Result<Registration?>> GetByIdAsync(Guid id);
        Task<Result<Registration>> CreateAsync(Registration course);
        Task<Result<Registration>> UpdateAsync(Registration course);
        Task<Result<Registration>> RemoveAsync(Registration course);
    }
}
