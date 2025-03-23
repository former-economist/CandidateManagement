using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Infrastructure.Entity
{
    public class Centre
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required bool Certified { get; set; }
        public required string TelephoneNumber { get; set; }

        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
        //public virtual ICollection<Course> Courses { get; } = new List<Course>();
    }
}
