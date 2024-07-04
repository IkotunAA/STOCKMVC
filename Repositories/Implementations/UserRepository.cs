using Microsoft.EntityFrameworkCore;
using STOCKMVC.Context;
using STOCKMVC.Entities;
using STOCKMVC.Repositories.Interfaces;
using System.Linq.Expressions;

namespace STOCKMVC.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        protected readonly StockDbContext _context;
        public UserRepository(StockDbContext context)
        {
            _context = context;
        }
        public User AddUser(User user)
        {
            _context.Users.Add(user);
            return user;
        }

        public bool Exist(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public User FindByEmail(string email)
        {
            return _context.Users.Include(x => x.UserRoles).ThenInclude(r => r.Role).SingleOrDefault(x => x.Email.ToLower() == email.ToLower());
        }

        public User FindById(string id)
        {
            return _context.Users.Find(id);
        }

        public User FindByUserName(string userName)
        {
            return _context.Users.Include(x => x.UserRoles).ThenInclude(r => r.Role).SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
        }

        public User GetUser(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<User> GetUser(Expression<Func<User, bool>> predicate = null)
        //{
        //    throw new NotImplementedException();
        //}

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
