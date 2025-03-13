﻿using System;
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
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required bool Certified { get; set; }
        public required string TelephoneNumber { get; set; }

        public ICollection<Registration> Candidates { get;} = new List<Registration>();
        public ICollection<Registration> Courses { get; } = new List<Registration>();
    }
}
