using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagement.Infrastructure.Entity;
using CandidateManagement.Models;

namespace CandidateManagement.Services.Interfaces
{
    public interface ICentreService
    {
        Task<IEnumerable<Registration>> GetAllAsync();
        Task<Result<Registration?>> GetByIdAsync(Guid id);
        Task<Result<Registration>> CreateAsync(Registration centre);
        Task<Result<Registration>> UpdateAsync(Registration centre);
        Task<Result<Registration>> RemoveAsync(Registration centre);
    }
}
