using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TalentBridgePortal.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public IFormFile Resume { get; set; }
    }
}
