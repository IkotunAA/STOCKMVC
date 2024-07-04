using STOCKMVC.Enum;

namespace STOCKMVC.Entities
{
    public class Role : Auditables
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
