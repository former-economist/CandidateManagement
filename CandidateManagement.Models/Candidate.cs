using System.ComponentModel.DataAnnotations;

namespace CandidateManagement.Models

{
    public class Candidate
    {
        [Key]
        public Guid Id { get; set; }

        [Required (ErrorMessage = "Forename required")]
        public string Forename { get; set; } = "";

        [Required(ErrorMessage = "Surname required")]
        public string Surname { get; set; } = "";

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Date of Birth required")]
        public DateTime DateOfBirth { get; set; }
        public string SwqrNumber { get; set; } = "";
    }
}
