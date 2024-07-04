namespace STOCKMVC.Entities
{
    public class UserRole : Auditables
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
