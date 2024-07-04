using STOCKMVC.Entities;
using STOCKMVC.Enum;

namespace STOCKMVC.Models
{
    public class UserResponse : BaseResponse
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; }
    }
}
