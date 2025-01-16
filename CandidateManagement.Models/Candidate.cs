namespace CandidateManagement.Models

{
    public class Candidate
    {
        public Guid Id { get; set; }
        public string Forename { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string SwqrNumber { get; set; } = "";
    }
}
