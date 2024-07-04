using STOCKMVC.Entities;

namespace STOCKMVC.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Role AddRole(Role role);
        Role GetRole(string Name);
        IReadOnlyCollection<Role> GetRoles();
    }
}
