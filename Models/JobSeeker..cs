using System.ComponentModel.DataAnnotations;

namespace TalentBridgePortal.Models
{
    public class JobSeeker
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string ResumePath { get; set; }
    }
}
