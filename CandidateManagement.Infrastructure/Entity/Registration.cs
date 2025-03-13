using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Infrastructure.Entity
{
    public class Registration
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public Guid CandidateId { get; set; }
        public Registration Candidate { get; set; }
        public Guid CourseId { get; set; }
        public Registration Course { get; set; }
    }
}
