using STOCKMVC.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace STOCKMVC.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
