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

        public ICollection<Registration> Centres { get; } = new List<Registration>();
        public ICollection<Registration> Registrations { get; } = new List<Registration>();
    }
}
