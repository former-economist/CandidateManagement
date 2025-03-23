using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagement.Infrastructure.Entity;

namespace CandidateManagement.Repositories.Interfaces
{
    public interface ICentreRepository: IBaseRepository<Centre>
    {
        Task<Centre> GetByEmailAsync(string email);
    }

}
