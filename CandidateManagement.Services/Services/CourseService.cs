using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagement.Infrastructure.Entity;
using CandidateManagement.Models;
using CandidateManagement.Services.Interfaces;

namespace CandidateManagement.Services.Services
{
    public class CourseService : ICourseService
    {
        public Task<Result<Registration>> CreateAsync(Registration course)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Registration>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<Registration?>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Registration>> RemoveAsync(Registration course)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Registration>> UpdateAsync(Registration course)
        {
            throw new NotImplementedException();
        }
    }
}
