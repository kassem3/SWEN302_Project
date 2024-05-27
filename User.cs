using System.ComponentModel.DataAnnotations;

namespace SimpleJobPortal.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; } // Admin, Employer, JobSeeker
    }
}
