namespace CandidateManagement.Infrastructure.Entity

{
    public class Registration
    {
        public Guid Id { get; set; }
        public required string Forename { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string SwqrNumber { get; set; }
        public required string TelephoneNumber { get; set; }

        public Guid CentreID { get; set; }
        public Registration Centre { get; set; } = null!;
        public ICollection<Registration> Registrations { get; } =  new List<Registration>();
    }
}
