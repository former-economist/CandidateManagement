using System.ComponentModel.DataAnnotations;

namespace CandidateManagement.Models

{
    public class Candidate
    {
        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Forename required")]
        [StringLength(50, ErrorMessage = "Candidate Forename cannot be longer than 50 characters")]
        public string Forename { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Surname required")]
        [StringLength(50, ErrorMessage = "Candidate Surname cannot be longer than 50 characters")]
        public string Surname { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Date of Birth required")]
        public DateTime DateOfBirth { get; set; }
        public string SwqrNumber { get; set; } = "";
    }
}
