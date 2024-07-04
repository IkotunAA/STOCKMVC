using STOCKMVC.Enum;
using System.Data;
using System.Reflection;

namespace STOCKMVC.Entities
{
    public class User : Auditables
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public Gender Gender { get; set; }
        public decimal Wallet { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
