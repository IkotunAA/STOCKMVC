using STOCKMVC.Entities;
using System.Linq.Expressions;

namespace STOCKMVC.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User AddUser(User user);
        User UpdateUser(User user);
        User FindByUserName(string userName);
        User FindById(string id);
        User FindByEmail(string email);
        User GetUser(Expression<Func<User, bool>> predicate);
        //IEnumerable<User> GetUser(Expression<Func<User, bool>> predicate = null);
        bool Exist(Expression<Func<User, bool>> predicate);
    }
}
