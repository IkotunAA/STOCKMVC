using STOCKMVC.Models;
using STOCKMVC.Models.DTOs;

namespace STOCKMVC.Services.Interfaces
{
    public interface IIdentityService
    {
        //Register
        BaseResponse Register(RegisterModel model);

        //Login
        UserResponse Login(LoginModel model);

        UserResponse GetUser(string userName);
        //Manage Account

        BaseResponse AddRole(CreateRoleModel model);
        IEnumerable<RoleDTO> GetRoles();
    }
}
