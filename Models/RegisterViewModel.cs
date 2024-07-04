using STOCKMVC.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace STOCKMVC.Models
{
    public class RegisterViewModel
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
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }
        [Required]
        public List<SelectListItem> Roles { get; set; }
        public string SelectedRole { get; set; }
    }
}
