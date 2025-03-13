using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Infrastructure.Entity
{
    public class Course
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Centre> Centres { get; } = new List<Centre>();
        public ICollection<Registration> Registrations { get; } = new List<Registration>();
    }
}
