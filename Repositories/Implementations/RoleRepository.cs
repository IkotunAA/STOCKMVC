using STOCKMVC.Context;
using STOCKMVC.Entities;
using STOCKMVC.Repositories.Interfaces;

namespace STOCKMVC.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly StockDbContext _context;
        public RoleRepository(StockDbContext context) 
        { 
            _context = context;
        }
        public Role AddRole(Role role)
        {
            _context.Roles.Add(role);
            return role;

        }

        public Role GetRole(string Name)
        {
            return _context.Roles.FirstOrDefault(x => x.Name == Name);
        }

        public IReadOnlyCollection<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
