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
        Task<IEnumerable<Centre>> GetAllAsync();
        Task<Result<Centre?>> GetByIdAsync(Guid id);
        Task<Result<Centre>> CreateAsync(Centre centre);
        Task<Result<Centre>> UpdateAsync(Centre centre);
        Task<Result<Centre>> RemoveAsync(Centre centre);
    }
}
